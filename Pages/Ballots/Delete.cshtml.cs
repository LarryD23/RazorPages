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
    public class DeleteModel : PageModel
    {
        private readonly Voting_Final.Models.DomainContext _context;

        public DeleteModel(Voting_Final.Models.DomainContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ballot = await _context.Ballot.FindAsync(id);
            if (ballot != null)
            {
                Ballot = ballot;
                _context.Ballot.Remove(Ballot);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
