using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DigitalCards.Data;
using DigitalCards.models;

namespace DigitalCards.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly DigitalCardsContext _context;

        public DetailsModel(DigitalCardsContext context)
        {
            _context = context;
        }

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

            var card = await _context.Card.FirstOrDefaultAsync(m => m.CardID == id);
            if (card == null)
            {
                return NotFound();
            }
            else
            {
                Card = card;


            }
            
        
            string? cookieValue = Request.Cookies["MyCookieCard"];
            // Check if the product ID is valid

            if (cookieValue == null)
            {
                // Create a new cookie if it doesn't exist
                CookieOptions options = new CookieOptions
                {
                    Expires = DateTime.Now.AddHours(24),
                    SameSite = SameSiteMode.Strict,
                    Domain = "localhost"
                };

                Response.Cookies.Append("MyCookieCard", Card.CardID.ToString(), options);
            }
            else
            {
                // Append the new product ID to the existing cookie value
                CookieOptions options = new CookieOptions
                {
                    Expires = DateTime.Now.AddHours(24),
                    SameSite = SameSiteMode.Strict,
                    Domain = "localhost"
                };
                // Update the cookie
                Response.Cookies.Append("MyCookieCard", cookieValue += "," + Card.CardID.ToString(), options);
            }


            // Redirect to the home page after updating the cart
            return RedirectToPage("/Index");
        }

        
            
        }
    }
