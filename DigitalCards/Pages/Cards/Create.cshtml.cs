using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DigitalCards.Data;
using DigitalCards.models;
using Microsoft.AspNetCore.Authorization;

namespace DigitalCards.Pages.Cards
{
    [Authorize]
    public class CreateModel : PageModel
    {

        private readonly DigitalCards.Data.DigitalCardsContext _context;
        private readonly IWebHostEnvironment _env;

        [BindProperty]
        public Card Card { get; set; } = default!;

        [BindProperty]
        public IFormFile ImageUpload { get; set; } = default!;
        public CreateModel(DigitalCards.Data.DigitalCardsContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult OnGet()
        {
            return Page();
        }        
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (ImageUpload != null)
            {
                string imageFileName = DateTime.Now.ToString("yyy-MM-dd-HH-mm-ss" ) + ImageUpload.FileName;

                //Set Defaults for Card
                Card.ImageName = imageFileName;


                if (!ModelState.IsValid || _context.Card == null || Card == null)
                {
                    return Page();
                }

                //save carde to database
                _context.Card.Add(Card);

                //Save teh dbcontext in the database
                await _context.SaveChangesAsync();

                string filePath = Path.Combine(_env.WebRootPath, "images", imageFileName);
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageUpload.CopyToAsync(fileStream);
                }

                return RedirectToPage("./Index");
            }
            else
            {
                // Handle the case when ImageUpload is null
                // You can return an error message or redirect to the Create page with an error.
                ModelState.AddModelError("ImageUpload", "Please select an image to upload.");
                return Page();
            }
           
        }
    }
}
