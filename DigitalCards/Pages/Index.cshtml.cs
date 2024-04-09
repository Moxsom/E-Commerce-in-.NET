using DigitalCards.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DigitalCards.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DigitalCards.Data.DigitalCardsContext _context;

        public IndexModel(ILogger<IndexModel> logger, Data.DigitalCardsContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IList<Card> Card { get; set; } = default!;
        public async Task OnGetAsync()
        {
            if (_context.Card != null)
            {
                Card = await _context.Card.ToListAsync();
            }   
        }
    }
}