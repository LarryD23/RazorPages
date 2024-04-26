﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Voting_Final.Models;

namespace Vote_Final.Pages.Ballots
{
    public class CreateModel : PageModel
    {
        private readonly Voting_Final.Models.DomainContext _context;

        public CreateModel(Voting_Final.Models.DomainContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Ballot Ballot { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            _context.Ballot.Add(Ballot);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
