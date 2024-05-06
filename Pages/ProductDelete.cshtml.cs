using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoExampleAndy.Data;
using ToDoExampleAndy.Model;
using System.Threading.Tasks;
using System.Linq;

namespace ToDoExampleAndy.Pages
{
    public class ProductDeleteModel : PageModel
    {
        // A private field to hold the database context. This is used to interact with the database.
        private readonly AppDbContext _dbConnection;
        //A variable of the type ProductsModel to store the retrieved data
        public ProductModel Item { get; set; }
        //Constructor injecting the database context
        public ProductDeleteModel(AppDbContext context)
        {
            //Assign the database context to _dbConnection
            _dbConnection = context;
        }
        //OnGet method that is called when the page is accessed
        public void OnGet(int id)
        {
            //Retrieve the Products from the Database based off of the ID given
            Item = _dbConnection.Products.FirstOrDefault(t => t.Guid == id);
        }
        //Runs on submitting of the form
        public async Task<IActionResult> OnPostAsync(int id)
        {
            //Retrieve the Products from the Database based off of the ID given
            var itemToDelete = _dbConnection.Products.FirstOrDefault(t => t.Guid == id);
            //If a Product is returned
            if (itemToDelete != null)
            {
                //Delete the Product from the database context
                _dbConnection.Products.Remove(itemToDelete);
                //Save changes to the database
                await _dbConnection.SaveChangesAsync();
                //Return to the home page
                return RedirectToPage("Index");
            }
            //If no Product is returned (likely due to being an incorrect ID)
            else
            {
                //Return NotFound() to the page
                return NotFound();
            }
        }
    }
}
