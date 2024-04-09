using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitalCards.Pages
{
    public class ConfirmationModel : PageModel
    {
        [BindProperty]
        public string ConfirmationNumber { get; set; } 
        public void OnGet(string confirmationNumber)
        {
            ConfirmationNumber = confirmationNumber;
        }
    }
}
