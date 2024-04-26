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
    public class DetailsModel : PageModel
    {
        private readonly Voting_Final.Models.DomainContext _context;

        public DetailsModel(Voting_Final.Models.DomainContext context)
        {
            _context = context;
        }

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
    }
}
