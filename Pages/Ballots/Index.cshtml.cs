using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Voting_Final.Models;
using Vote_Final.Utilities;
using System.Text.Json;
using NuGet.Packaging.Signing;
using Vote_Final.Models;
using Vote_Final.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Http;

namespace Vote_Final.Pages.Ballots
{
    public class IndexModel : PageModel
    {
        private readonly Voting_Final.Models.DomainContext _context;

        public const string SessionKeyName = "_Name";
        public const string SessionKeyId = "_Id";

      

   

        public IndexModel(Voting_Final.Models.DomainContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BallotModel BallotModel { get; set; }

        [BindProperty]
        public BallotIssueSelection BallotIssueSelection { get; set; }

        [BindProperty]
        public RaceBallotSelection RaceBallotSelection { get; set; }

        [BindProperty]
        public Dictionary<int, int> SelectedCandidateIds { get; set; }
        [BindProperty]
        public Dictionary<string, bool> IsFors { get; set; } = new Dictionary<string, bool>();//populate prior to rendering page 

        //public Dictionary<int, bool> SelectedBallotIssues { get; set; } = new Dictionary<int, bool>();


        public async Task<IActionResult> OnGet(int? id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyName)))
            {
                //HttpContext.Session.SetString(SessionKeyName, "John Smith");

                //Get next available voterId
                var firstVoter = await _context.Voters.OrderBy(v => v.VoterId)
                .FirstOrDefaultAsync(v => v.IsAlive && v.VoterStatus == "Active");

                //int nextVoterId = lastVoter != null ? lastVoter.VoterId : 1;

                if (firstVoter != null)
                {
                    // Set SessionKeyName to the VoterName associated with the VoterId
                    HttpContext.Session.SetString(SessionKeyName, firstVoter.VoterName);
                    HttpContext.Session.SetInt32(SessionKeyId, firstVoter.VoterId);
                }
                else
                {
                    return NotFound();
                }
            }
            var name = HttpContext.Session.GetString(SessionKeyName);
            var voterId = HttpContext.Session.GetInt32(SessionKeyId).ToString();


            if (id == null)
            {
                var latestBallotId = await GetLatestBallotId();
                return RedirectToPage("/Ballots/Index", new { id = latestBallotId });
            }

 

            var ballot = await _context.Ballot.FirstOrDefaultAsync(m => m.BallotId == id);

            if (ballot == null)
            {
                return NotFound();
            }

            BallotModel= BallotModel.BallotModelRefactory(ballot, _context);

            IsFors = BallotModel.IsFors;
            RaceBallotSelection = new RaceBallotSelection();
            return Page();
         

        }

        //POST handler for submitting Ballot
        public async Task<IActionResult> OnPost()
        {
            int? voterId = HttpContext.Session.GetInt32(SessionKeyId);

            if(!voterId.HasValue)
            {
                return NotFound();
            }

            //verify voterid exists in db
            var voterExists = await _context.Voters.AnyAsync(v => v.VoterId == voterId);

            if(!voterExists)
            {
                return NotFound();
            }

         

            // Update the session with the new voterId
            HttpContext.Session.SetInt32(SessionKeyId, voterId.Value);

            if (SelectedCandidateIds != null && SelectedCandidateIds.Count > 0 && voterId != null)
            {
                foreach (var raceId in SelectedCandidateIds.Keys)
                {
                    var candidateId = SelectedCandidateIds[raceId];

                    var raceBallotSelection = new RaceBallotSelection
                    {
                        RaceId = raceId,
                        CandidateId = candidateId,
                        BallotId = await GetLatestBallotId(),
                        TimeStamp = DateTime.Now,
                        VoterId = voterId.Value,
                    };

                    
                    _context.RaceBallotSelection.Add(raceBallotSelection);
                    await _context.SaveChangesAsync();
                }

                
            }

            if (IsFors != null && IsFors.Count > 0)
            {
                foreach (var key in IsFors.Keys)
                {
                    if (key.StartsWith("ballotIssue")  && (key.EndsWith("_Yes") || key.EndsWith("_No")))
                    {
                        //extract ballotissueid from the key
                        var issueId = key.Replace("ballotIssue", "").Replace("_Yes", "").Replace("_No", "");
                        int parsedIssueId = int.Parse(issueId);
                        if(parsedIssueId != null)
                        {
                            var isFor = IsFors[key];
                            var ballotIssueSelection = new BallotIssueSelection
                            {
                                BallotId = await GetLatestBallotId(),
                                VoterId = voterId.Value,
                                BallotIssueId = parsedIssueId,
                                IsFor = isFor,
                                TimeStamp = DateTime.Now,
                            };
                            _context.BallotIssueSelection.Add(ballotIssueSelection);
                            await _context.SaveChangesAsync();
                        }
                    }
                }

            }

            
            var nextVoter = await _context.Voters.OrderBy(v => v.VoterId)
                .FirstOrDefaultAsync(v => v.VoterId > voterId.Value && v.IsAlive && v.VoterStatus == "Active");

            if (nextVoter != null)
            {
                HttpContext.Session.SetString(SessionKeyName, nextVoter.VoterName);
                HttpContext.Session.SetInt32(SessionKeyId, nextVoter.VoterId);

                return RedirectToPage("/Ballots/Index");
            }
            else
            {
                ViewData["Message"] = "All eligible voters have voted. Thank you!";
            }

            return RedirectToPage("Success");
        }





        public async Task<int> GetLatestBallotId()
        {
            var latestBallot = await _context.Ballot.OrderByDescending(b => b.BallotId).FirstOrDefaultAsync();
            return latestBallot?.BallotId ?? 0;
        }

      

      
    }
}

