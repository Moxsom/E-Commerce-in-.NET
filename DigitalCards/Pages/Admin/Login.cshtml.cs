using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DigitalCards.Pages.Admin
{
    public class LoginModel : PageModel
    {
        //private readonly DigitalCards.Data.DigitalCardsContext _context;
        private readonly ILogger<LoginModel> _logger;
        private readonly IConfiguration _configuration;

        [BindProperty]
        public string Username1 { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public LoginModel(IConfiguration configuration, ILogger<LoginModel> logger)
        {
            //_context = context;
            _logger = logger;
            _configuration = configuration;
        }
        
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //Get the credentials form appsettings.json -- THIS IS NOT TAKING FROM JSON FILE, username = 'brent' fix this. 
<<<<<<< HEAD
            string usernameCreds = _configuration["Username1"];
=======
            string usernameCreds = _configuration["Username"];
>>>>>>> ac463099319573726fe27df66cb70b22af98e5d3
            string passwordCreds = _configuration["Password"];

            //Get the user Input for login
            string usernameInput = Username1;
            string passwordInput = Password;

            //Compare input against credentials. 
            if (usernameInput == usernameCreds && passwordInput == passwordCreds)
            {
                // Setup session
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Username1.ToString()),
                new Claim("Username1", Username1),
                new Claim(ClaimTypes.Role, "User")
            };

                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties());

                return RedirectToPage("/Cards/Index");
                //If they match, log the user in and redirect to the admin page                
                
            }
            else
            {
                //If they don't match, log the attempt and return to the login page
                _logger.LogInformation("Login failed for user {Username}", usernameInput);
                return Page();
            }           


        }

    }
}
