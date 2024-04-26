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
    public class IndexModel : PageModel
    {
        private readonly Voting_Final.Models.DomainContext _context;

        public IndexModel(Voting_Final.Models.DomainContext context)
        {
            _context = context;
        }

        public IList<Race> Race { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Race = await _context.Race.ToListAsync();
        }
    }
}
