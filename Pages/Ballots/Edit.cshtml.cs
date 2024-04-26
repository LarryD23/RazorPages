using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Voting_Final.Models;

namespace Vote_Final.Pages.Ballots
{
    public class EditModel : PageModel
    {
        private readonly Voting_Final.Models.DomainContext _context;

        public EditModel(Voting_Final.Models.DomainContext context)
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

            var ballot =  await _context.Ballot.FirstOrDefaultAsync(m => m.BallotId == id);
            if (ballot == null)
            {
                return NotFound();
            }
            Ballot = ballot;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Ballot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BallotExists(Ballot.BallotId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BallotExists(int id)
        {
            return _context.Ballot.Any(e => e.BallotId == id);
        }
    }
}
