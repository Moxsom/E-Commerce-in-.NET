using DigitalCards.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DigitalCards.Pages
{
    public class ShoppingCartModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger; // This is a field
        private readonly DigitalCards.Data.DigitalCardsContext _context; // This is a field

        public string[] CartProductIds { get; private set; } = Array.Empty<string>();// This is a property

        public ShoppingCartModel(ILogger<IndexModel> logger, Data.DigitalCardsContext context) // This is the constructor
        {
            _logger = logger; // This is an assignment
            _context = context; //  This is an assignment
        }

        public IList<Card> Card { get; set; } = default!;// This is a property
        public async Task OnGetAsync() // This is a method
        {
            if (_context.Card != null) // This is a conditional statement
            {
                Card = await _context.Card.ToListAsync(); // This is an assignment
            }

            string? cookieValue = Request.Cookies["MyCookieCard"];

            if (string.IsNullOrEmpty(cookieValue))
            {
                // No products in the cart
                ViewData["Message"] = "YOUR CART IS EMPTY";
                
            } else
            {
                CartProductIds = cookieValue.Split(',');
            }

            // Parse the cookie value into an array of product IDs
            
            
        }
    }
}
