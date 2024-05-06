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
        //Database context for accessing the database
        private readonly AppDbContext _context;

        //User model bound to the form input
        [BindProperty]
        public UserModel User { get; set; }

        // Constructor injecting the database context
        public RegisterModel(AppDbContext context)
        {
            //Set the // Assign the database context instance to the private field _dbConnection
            _context = context;
        }

        //OnGet method that is called when the page is accessed
        public void OnGet()
        {
        }

        // POST handler for form submission
        public IActionResult OnPost()
        {
            //Check if the model state is valid
            if (!ModelState.IsValid)
            {
                //Return to the page if validation fails
                return Page();
            }
            //Hash the password before saving
            User.Password = HashPassword(User.Password);
            //Add the new user to the database
            _context.Users.Add(User);
            //Commit the changes to the database
            _context.SaveChanges();
            //Redirect to the login page on successful registration
            return RedirectToPage("Login");
        }

        // Method to hash the password using a secure hash algorithm
        private string HashPassword(string password)
        {
            //Generate a 128-bit salt
            byte[] salt = new byte[128 / 8];
            //Use the RandomNumberGenerator function to create a random number
            using (var rng = RandomNumberGenerator.Create())
            {
                //Fill the salt with cryptographically strong random bytes
                rng.GetBytes(salt);
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
