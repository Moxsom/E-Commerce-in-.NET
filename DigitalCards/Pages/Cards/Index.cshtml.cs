using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DigitalCards.Data;
using DigitalCards.models;
using Microsoft.AspNetCore.Authorization;


namespace DigitalCards.Pages.Cards
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly DigitalCards.Data.DigitalCardsContext _context;

        public IndexModel(DigitalCards.Data.DigitalCardsContext context)
        {
            _context = context;
        }

        public IList<Card> Card { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Card != null)
            {
                Card = await _context.Card.ToListAsync();
            }
        }
    }
}
