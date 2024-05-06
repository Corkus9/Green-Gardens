using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoExampleAndy.Data;
using ToDoExampleAndy.Model;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ToDoExampleAndy.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly AppDbContext _context; // Database context for accessing the database

        [BindProperty]
        public UserModel User { get; set; } // User model bound to the form input

        // Constructor injecting the database context
        public RegisterModel(AppDbContext context)
        {
            _context = context;
        }

        // GET handler to initialize any state needed for the form
        public void OnGet()
        {
        }

        // POST handler for form submission
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) // Check if the model state is valid
            {
                return Page(); // Return to the page if validation fails
            }

            User.Password = HashPassword(User.Password); // Hash the password before saving
            _context.Users.Add(User); // Add the new user to the database
            _context.SaveChanges(); // Commit the changes to the database
            return RedirectToPage("Login"); // Redirect to the login page on successful registration
        }

        // Method to hash the password using a secure hash algorithm
        private string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8]; // Generate a 128-bit salt
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt); // Fill the salt with cryptographically strong random bytes
            }

            // Return the hashed password as a base64 string
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }
    }
}
