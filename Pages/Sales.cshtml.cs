using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using ToDoExampleAndy.Data;
using ToDoExampleAndy.Model;
using Microsoft.IdentityModel.Tokens;

namespace ToDoExampleAndy.Pages
{
    public class SalesViewModel : PageModel
    {
        // A private field to hold the database context. This is used to interact with the database.
        private readonly AppDbContext _db;

        // A private field for logging. ILogger is used for logging different types of information.
        private readonly ILogger<SalesModel> _logger;
        //A variable of the type SalesViewModel to store the retrieved data
        public SalesViewModel Item { get; set; }
        //A list of all returned Sales from the database
        public List<SalesModel> Sales { get; set; }

        // A private field to hold the database context. This is used to interact with the database.
        private readonly AppDbContext _dbConnection;

        // Constructor for the IndexModel class.
        // It takes an ILogger and an instance of AppDbContext as parameters.
        public SalesViewModel(ILogger<SalesModel> logger, AppDbContext _db)
        {
            // Assign the logger instance to the private field _logger.
            _logger = logger;

            // Assign the database context instance to the private field _dbConnection.
            _dbConnection = _db;
        }

        //OnGet method that is called when the page is accessed
        public void OnGet()
        {
            //Retrieve the list of Sales and store it
            Sales = _dbConnection.Sales.ToList();
            //Try to run this code
            try
            {
                ////Get the data from the User Claim as an array and store it as DataGet
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
    }
}


