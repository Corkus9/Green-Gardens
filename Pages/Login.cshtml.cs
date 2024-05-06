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
        //Database context for accessing the database
        private readonly AppDbContext _context;
        //Email bound to the form input
        [BindProperty]
        public string Email { get; set; }
        //Password bound to the form input
        [BindProperty]
        public string Password { get; set; }

        //Constructor injecting the database context
        public LoginModel(AppDbContext context)
        {
            //Assign the database context to _context
            _context = context;
        }
        //OnGet method that is called when the page is accessed
        public void OnGet()
        {
        }
        //Runs on submitting of the form
        public async Task<IActionResult> OnPostAsync()
        {
            //Check if the model state is valid
            if (!ModelState.IsValid)
            {
                // Return to the page if validation fails
                return Page();
            }
            //Get the User object from the database where the email is the same
            var user = _context.Users.FirstOrDefault(u => u.Email == Email);

            //Checks that the Email search has returned a user and then checks the password matches
            if (user != null && VerifyPassword(Password, user.Password))
            {
                //Get the data and store the desired data in the Claim
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                        {
                                //Store Fname to Name
                                new(ClaimTypes.Name, user.Fname),
                                //Store SName to Surname
                                new(ClaimTypes.Surname, user.Sname),
                                //Store Email to Email
                                new(ClaimTypes.Email, user.Email),
                                //Store the Role string to Role
                                new(ClaimTypes.Role, user.Admin)
                            },
                        CookieAuthenticationDefaults.AuthenticationScheme)),
                    new AuthenticationProperties()
                );
                //Redirect to the Index page after successful login
                return RedirectToPage("Index");
            }
            //Or provide a user-friendly error message
            return Page();
        }

        private bool VerifyPassword(string providedPassword, string storedHash)
        {
            //Placeholder to apply Password Validation logic if needed
            return true;
        }
    }
}
