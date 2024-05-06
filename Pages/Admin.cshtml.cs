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

        private readonly AppDbContext _db;

        // A private field for logging. ILogger is used for logging different types of information.
        private readonly ILogger<AdminModel> _logger;

        //A list of all returned Products from the database
        public List<ProductModel> Products { get; set; }

        // A private field to hold the database context. This is used to interact with the database.
        private readonly AppDbContext _dbConnection;

        // Constructor for the IndexModel class.
        // It takes an ILogger and an instance of AppDbContext as parameters.
        public AdminModel(ILogger<AdminModel> logger, AppDbContext _db)
        {
            // Assign the logger instance to the private field _logger.
            _logger = logger;

            // Assign the database context instance to the private field _dbConnection.
            _dbConnection = _db;
        }

        // OnGet method that is called when the page is accessed.
        public void OnGet()
        {
            //Retrieve the Products from the Database connection and store them as a list
            Products = _dbConnection.Products.ToList();
        }
    }
}