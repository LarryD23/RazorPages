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

namespace Vote_Final.Pages.Ballots
{
    public class IndexModel : PageModel
    {
        private readonly Voting_Final.Models.DomainContext _context;



        public IndexModel(Voting_Final.Models.DomainContext context)
        {
            _context = context;
        }

        //[BindProperty]
        //public Dictionary<int, (int CandidateId, bool IsFor)> UserSelections { get; set; }

        //[BindProperty]
        //public BallotSelection BallotSelection { get; set; }


        //public string GetCandidateName(int candidateId)
        //{
        //    var candidate = _context.Candidate.FirstOrDefault(c => c.CandidateId == candidateId);
        //    return candidate != null ? $"{candidate.FirstName} {candidate.LastName}" : "Candidate Not Found";
        //}



        public async Task OnGetAsync()
        {
            ViewData["Title"] = "Ballots";

            //Ballots = await _context.Ballot.ToListAsync();
            Races = await _context.Race.ToListAsync();
            Candidates = await _context.Candidate.ToListAsync();
            BallotIssues = await _context.BallotIssue.ToListAsync();
            CandidateRetrieval = RaceUtility.GetCandidatesForRaces(_context, await GetLatestBallotId());
            BallotIssueRetrieval = RaceUtility.GetBallotIssuesForBallots(_context, await GetLatestBallotId());
            _context.Database.EnsureCreated();

            int latestBallotId = await GetLatestBallotId();

            var validBallot = Ballots.FirstOrDefault(b => b.BallotId == latestBallotId);
            if (validBallot == null)
            {
                throw new ArgumentException("Invalid BallotId");
            }

        }

        //POST handler for submitting Ballot
        public async Task<IActionResult> OnPostSubmitBallotAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int ballotId = await GetLatestBallotId();

            // Create a new BallotSelection object and bind form data to it
            //var ballotSelection = new BallotSelection();
            if (await TryUpdateModelAsync(ballotSelection, "BallotSelection"))
            {
                // Validation successful, proceed with saving to the database
                ballotSelection.BallotId = ballotId;
                ballotSelection.TimeStamp = DateTime.UtcNow;

                _context.BallotSelection.Add(ballotSelection);
                await _context.SaveChangesAsync();

                return new RedirectToPageResult("/Ballots/Index");
            }
            else
            {
                // Model binding failed, return a bad request
                return BadRequest(ModelState);
            }
        }





        public async Task<int> GetLatestBallotId()
        {
            var latestBallot = await _context.Ballot.OrderByDescending(b => b.BallotId).FirstOrDefaultAsync();
            return latestBallot?.BallotId ?? 0;
        }



        public IList<Ballot> Ballots { get; set; } = default!;
        public IList<Race> Races { get; set; } = default!;

        public IList<Candidate> Candidates { get; set; } = default!;

        public IList<BallotIssue> BallotIssues { get; set; } = default!;

        public Dictionary<int, List<int>> CandidateRetrieval { get; set; }

        public Dictionary<int, List<BallotIssue>> BallotIssueRetrieval { get; set; }

        public string GetCandidateName(int candidateId)
        {
            var candidate = _context.Candidate.FirstOrDefault(c => c.CandidateId == candidateId);
            return candidate != null ? $"{candidate.FirstName} {candidate.LastName}" : "Candidate Not Found";
        }

    }
}

