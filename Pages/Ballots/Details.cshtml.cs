using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Voting_Final.Models;

namespace Vote_Final.Pages.Ballots
{
    public class DetailsModel : PageModel
    {
        private readonly Voting_Final.Models.DomainContext _context;

        public DetailsModel(Voting_Final.Models.DomainContext context)
        {
            _context = context;
        }

        public Ballot Ballot { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ballot = await _context.Ballot.FirstOrDefaultAsync(m => m.BallotId == id);
            if (ballot == null)
            {
                return NotFound();
            }
            else
            {
                Ballot = ballot;
            }
            return Page();
        }
    }
}
