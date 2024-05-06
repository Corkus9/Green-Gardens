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

        private readonly AppDbContext _db;

        // A private field for logging. ILogger is used for logging different types of information.
        private readonly ILogger<SalesModel> _logger;

        // Public property for Username. This can be accessed and modified from outside the class.
        public string Username { get; set; }

        public SalesViewModel Item { get; set; }

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

        // OnGet method that is called when the page is accessed.
        public void OnGet()
        {
            Sales = _dbConnection.Sales.ToList();
            try
            {
                Claim[] DataGet = HttpContext.User.Claims.ToArray();
                if (DataGet[3].Value != "Admin")
                {
                    Response.Redirect("/");
                    return;
                }
                ViewData["FName"] = DataGet[0].Value;
                ViewData["Admin"] = DataGet[3].Value;
            }
            catch
            {
                Response.Redirect("/Login");
                return;
            }
        }
    }
}


