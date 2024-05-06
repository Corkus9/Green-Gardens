using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoExampleAndy.Data;
using ToDoExampleAndy.Model;

namespace ToDoExampleAndy.Pages
{
    public class ProductCreateModel : PageModel
    {
        // A private field to hold the database context. This is used to interact with the database.
        private readonly AppDbContext _dbConnection;
        //A variable of the type ProductsModel to store the retrieved data
        public ProductModel Item { get; set; }
        //Constructor injecting the database context
        public ProductCreateModel(AppDbContext context)
        {
            //Assign the database context to _dbConnection
            _dbConnection = context;
        }
        //OnGet method that is called when the page is accessed
        public void OnGet()
        {
            //Initialize Item
            Item = new ProductModel();
        }
        //Runs on submitting of the form
        public IActionResult OnPost(ProductModel Item)
        {
            //Add the new Itme to the database context
            _dbConnection.Products.Add(Item);
            //Save the changes to the database
            _dbConnection.SaveChanges();
            //Return to the Admin Page
            return RedirectToPage("Admin");
        }
    }
}
