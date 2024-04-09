using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DigitalCards.Data;
using DigitalCards.models;
using Microsoft.AspNetCore.Authorization;

namespace DigitalCards.Pages.Cards
{
    [Authorize]
    public class EditModel : PageModel
    {

        private readonly DigitalCards.Data.DigitalCardsContext _context;

        public EditModel(DigitalCards.Data.DigitalCardsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Card Card { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Card == null)
            {
                return NotFound();
            }

            var card =  await _context.Card.FirstOrDefaultAsync(m => m.CardID == id);
            if (card == null)
            {
                return NotFound();
            }
            Card = card;
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

            _context.Attach(Card).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(Card.CardID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        private bool CardExists(int id)
        {
          return (_context.Card?.Any(e => e.CardID == id)).GetValueOrDefault();
        }
    }
}
