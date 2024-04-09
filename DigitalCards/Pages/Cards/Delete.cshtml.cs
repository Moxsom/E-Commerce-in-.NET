using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DigitalCards.Data;
using DigitalCards.models;

namespace DigitalCards.Pages.Cards
{
    public class DeleteModel : PageModel
    {
        private readonly DigitalCards.Data.DigitalCardsContext _context;

        public DeleteModel(DigitalCards.Data.DigitalCardsContext context)
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

            var card = await _context.Card.FirstOrDefaultAsync(m => m.CardID == id);

            if (card == null)
            {
                return NotFound();
            }
            else 
            {
                Card = card;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Card == null)
            {
                return NotFound();
            }
            var card = await _context.Card.FindAsync(id);

            if (card != null)
            {
                Card = card;
                _context.Card.Remove(Card);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
