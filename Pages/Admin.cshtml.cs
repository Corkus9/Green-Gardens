using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using ToDoExampleAndy.Data;
using ToDoExampleAndy.Model;
using Microsoft.IdentityModel.Tokens;

namespace ToDoExampleAndy.Pages
{
    public class AdminModel : PageModel
    {
        // A private field to hold the database context. This is used to interact with the database
        private readonly AppDbContext _db;

        // A private field for logging. ILogger is used for logging different types of information
        private readonly ILogger<AdminModel> _logger;
        //A variable of the type ProductsModel to store the retrieved data
        public ProductsModel Item { get; set; }

        //A list of all returned Products from the database
        public List<ProductModel> Products { get; set; }

        // A private field to hold the database context. This is used to interact with the database
        private readonly AppDbContext _dbConnection;

        //Constructor injecting the database context
        public AdminModel(ILogger<AdminModel> logger, AppDbContext _db)
        {
            // Assign the logger instance to the private field _logger
            _logger = logger;
            // Assign the database context instance to the private field _dbConnection
            _dbConnection = _db;
        }

        //OnGet method that is called when the page is accessed
        public void OnGet()
        {
            //Retrieve the Products from the Database connection and store them as a list
            Products = _dbConnection.Products.ToList();
            //Try to run this code
            try
            {
                //Get the data from the User Claim as an array and store it as DataGet
                Claim[] DataGet = HttpContext.User.Claims.ToArray();
                //If the Role value from the Claim is not "Admin"
                if (DataGet[3].Value != "Admin")
                {
                    //Redirect to the Login Page
                    Response.Redirect("/Login");
                    return;
                }
            }
            //If the code fails or causes an error (likely due to not being logged in so not returning data for the Claim) then run this instead
            catch
            {
                //Redirect to the Login Page
                Response.Redirect("/Login");
                return;
            }
        }

        //A function to update the Quantity of the Products requiring 2 Integers (The Product ID and the new Quantity)
        public async void UpdateQuantity(int NewQuant, int Guid)
        {
            //Get and store the Product from the database based off of the Guid
            var itemToUpdate = await _dbConnection.Products.FindAsync(Guid);
            //Set the Quantity value of the Product to the new quantity
            itemToUpdate.Quantity = NewQuant;
            //Save the changes to the database
            await _dbConnection.SaveChangesAsync();
        }
    }
}