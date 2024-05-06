using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoExampleAndy.Data;
using ToDoExampleAndy.Model;

namespace ToDoExampleAndy.Pages
{
    public class SaleCreateModel : PageModel
    {
        // A private field to hold the database context. This is used to interact with the database.
        private readonly AppDbContext _dbConnection;
        //A variable of the type SalesModel to store the retrieved data
        public SalesModel Item { get; set; }
        //Constructor injecting the database context
        public SaleCreateModel(AppDbContext context)
        {
            //Assign the database context to _dbConnection
            _dbConnection = context;
        }
        //OnGet method that is called when the page is accessed
        public void OnGet()
        {
            //Create the new Item as the type SalesModel
            Item = new SalesModel();
        }
        //OnPost method to run on submitting of the form
        public IActionResult OnPost(SalesModel Item)
        {
            //Set the TransactionDate for Item to the current DateTime
            Item.TransactionDate = DateTime.Now;
            //Add the Item to the Database Context
            _dbConnection.Sales.Add(Item);
            //Save the changes to the Database
            _dbConnection.SaveChanges();
            //Redirect to the Admin page
            return RedirectToPage("Admin");
        }
    }
}
