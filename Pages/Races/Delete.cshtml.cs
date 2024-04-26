using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Voting_Final.Models;

namespace Vote_Final.Pages.Races
{
    public class DeleteModel : PageModel
    {
        private readonly Voting_Final.Models.DomainContext _context;

        public DeleteModel(Voting_Final.Models.DomainContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Race Race { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var race = await _context.Race.FirstOrDefaultAsync(m => m.RaceId == id);

            if (race == null)
            {
                return NotFound();
            }
            else
            {
                Race = race;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var race = await _context.Race.FindAsync(id);
            if (race != null)
            {
                Race = race;
                _context.Race.Remove(Race);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
