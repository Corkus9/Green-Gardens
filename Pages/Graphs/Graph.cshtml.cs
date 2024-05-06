using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Text.Json;
using ToDoExampleAndy.Data;
using ToDoExampleAndy.Model;



namespace ToDoExampleAndy.Pages.Graphs
{
    public class GraphModel : PageModel
    {
        // A private field to hold the database context. This is used to interact with the database
        private readonly AppDbContext _dbConnection;
        //A list of all returned Sales from the database
        public List<SalesModel> Sales { get; set; }
        //Initialise a string ready to store the list of sales in a Json format
        public string SalesJson { get; set; }
        //Constructor injecting the database context
        public GraphModel(AppDbContext db)
        {
            // Assign the database context instance to the private field _dbConnection
            _dbConnection = db;
        }

        //OnGet method that is called when the page is accessed
        public void OnGet()
        {
            //Retrieve the Products from the Database connection and store them as a list
            Sales = _dbConnection.Sales.ToList();
            //Serialise the Sales data returned into a Json format of MM-dd-yyyy (to match American date format to work with the graphs)
            SalesJson = JsonSerializer.Serialize(Sales.Select(t => new { TransactionDate = t.TransactionDate.ToString("MM-dd-yyyy") }));
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
    }
}
