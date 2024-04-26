using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Voting_Final.Models;

namespace Vote_Final.Pages.Races
{
    public class EditModel : PageModel
    {
        private readonly Voting_Final.Models.DomainContext _context;

        public EditModel(Voting_Final.Models.DomainContext context)
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

            var race =  await _context.Race.FirstOrDefaultAsync(m => m.RaceId == id);
            if (race == null)
            {
                return NotFound();
            }
            Race = race;
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

            _context.Attach(Race).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RaceExists(Race.RaceId))
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

        private bool RaceExists(int id)
        {
            return _context.Race.Any(e => e.RaceId == id);
        }
    }
}
