using DigitalCards.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
//using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DigitalCards.Pages
{
    public class CheckoutModel : PageModel
    {
        [BindProperty, Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [BindProperty, Required, StringLength(100)]
        public string Address { get; set; } = string.Empty;

        [BindProperty, Required, StringLength(100)]
        public string PostalCode { get; set; } = string.Empty;

        [BindProperty, Required, StringLength(100)]
        public string City { get; set; } = string.Empty;

        [BindProperty, Required, StringLength(100)]
        public string Province { get; set; } = string.Empty;        

        [BindProperty, Required, StringLength(100)]
        public string Country { get; set; } = string.Empty;

        [BindProperty, Required, RegularExpression(@"^\d{16}$", ErrorMessage = "Credit Card Number must be 16 digits")]
        public string ccNumber { get; set; } = string.Empty;
        //  Regex modified from https://codepal.ai/regex-generator/query/TIJsaJoe/credit-card-expiration-regex
        [BindProperty, Required, RegularExpression(@"^(0[1-9]|1[0-2])(2[2-9]|[3-9][0-9])$", ErrorMessage = "Expiry Date must be in MMYY format")]
        public string ccExpiryDate { get; set; } = string.Empty;
        [BindProperty, Required, RegularExpression(@"^\d{3}$", ErrorMessage = "CVV must be 3 digits")]
        public string cvv { get; set; } = string.Empty;
        public IList<int> productIds { get; set; } = default!;

        //public string ConfirmationNumber { get; set; } = string.Empty;
        
        
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            string? cookieValue = Request.Cookies["MyCookieCard"];

            var jsonData = new
            {
                name = Name,
                address = Address,
                postalCode = PostalCode,
                city = City,
                province = Province,
                country = Country,
                ccNumber = ccNumber,
                ccExpiryDate = ccExpiryDate,
                cvv = cvv,
                products = cookieValue
            };

            var json = JsonConvert.SerializeObject(jsonData);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using(var client = new HttpClient())
            {
                var response = await client.PostAsync("https://nscc-inet2005-purchase-api.azurewebsites.net/purchase", data);

               if(response.IsSuccessStatusCode)
                {
                    var confirmationNumber = await response.Content.ReadAsStringAsync();
                    Response.Cookies.Delete("MyCookieCard");
                    return RedirectToPage("/Confirmation", new { confirmationNumber });
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    ViewData["Error"] = error;
                    return Page();
                }
            }

            //return RedirectToPage("/Index");
        }
    }
}
