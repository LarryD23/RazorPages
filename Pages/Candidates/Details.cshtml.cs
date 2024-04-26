using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Voting_Final.Models;

namespace Vote_Final.Pages.Candidates
{
    public class DetailsModel : PageModel
    {
        private readonly Voting_Final.Models.DomainContext _context;

        public DetailsModel(Voting_Final.Models.DomainContext context)
        {
            _context = context;
        }

        public Candidate Candidate { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await _context.Candidate.FirstOrDefaultAsync(m => m.CandidateId == id);
            if (candidate == null)
            {
                return NotFound();
            }
            else
            {
                Candidate = candidate;
            }
            return Page();
        }
    }
}
