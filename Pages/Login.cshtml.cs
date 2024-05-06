using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoExampleAndy.Data;
using ToDoExampleAndy.Model;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Principal;

namespace ToDoExampleAndy.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _context; // Database context for accessing the database

        [BindProperty]
        public string Email { get; set; } // Email bound to the form input

        [BindProperty]
        public string Password { get; set; } // Password bound to the form input

        // Constructor injecting the database context
        public LoginModel(AppDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) // Check if the model state is valid
            {
                return Page(); // Return to the page if validation fails
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == Email);

            // Implement password verification logic here
            if (user != null && VerifyPassword(Password, user.Password))
            {
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                        {
                                new(ClaimTypes.Name, user.Fname),
                                new(ClaimTypes.Surname, user.Sname),
                                new(ClaimTypes.Email, user.Email),
                                new(ClaimTypes.Role, user.Admin)
                            },
                        CookieAuthenticationDefaults.AuthenticationScheme)),
                    new AuthenticationProperties()
                );

                return RedirectToPage("Index"); // Redirect to the Index page after successful login
            }

            return Page(); // Or provide a user-friendly error message
        }

        private bool VerifyPassword(string providedPassword, string storedHash)
        {
            // Implement password verification logic here
            return true; // Placeholder
        }
    }
}
