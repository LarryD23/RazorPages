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

namespace Vote_Final.Pages.Ballots
{
    public class IndexModel : PageModel
    {
        private readonly Voting_Final.Models.DomainContext _context;



        public IndexModel(Voting_Final.Models.DomainContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BallotModel BallotModel { get; set; }

        [BindProperty]
        public BallotIssueSelection BallotIssueSelection {  get; set; }
        [BindProperty]
        public List<RaceBallotSelection> RaceBallotSelections { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
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

            return Page();
         

        }

        //POST handler for submitting Ballot
        public async Task<IActionResult> OnPostSubmitBallotAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BallotIssueSelection.VoterId = 1;
            RaceBallotSelections.VoterId = 1;

            _context.BallotIssueSelection.Add(BallotIssueSelection);
            _context.RaceBallotSelection.Add(RaceBallotSelection);
            
            var changes = await _context.SaveChangesAsync();

            

            return RedirectToPage("/Success");
        }





        public async Task<int> GetLatestBallotId()
        {
            var latestBallot = await _context.Ballot.OrderByDescending(b => b.BallotId).FirstOrDefaultAsync();
            return latestBallot?.BallotId ?? 0;
        }



        //public IList<Ballot> Ballots { get; set; } = default!;
        //public IList<Race> Races { get; set; } = default!;

        //public IList<Candidate> Candidates { get; set; } = default!;

        //public IList<BallotIssue> BallotIssues { get; set; } = default!;

        //public Dictionary<int, List<int>> CandidateRetrieval { get; set; }

        //public Dictionary<int, List<BallotIssue>> BallotIssueRetrieval { get; set; }

        //public string GetCandidateName(int candidateId)
        //{
        //    var candidate = _context.Candidate.FirstOrDefault(c => c.CandidateId == candidateId);
        //    return candidate != null ? $"{candidate.FirstName} {candidate.LastName}" : "Candidate Not Found";
        //}

    }
}

