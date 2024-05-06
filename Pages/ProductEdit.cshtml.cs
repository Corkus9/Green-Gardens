using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoExampleAndy.Data;
using ToDoExampleAndy.Model;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoExampleAndy.Pages
{
    public class ProductEditModel : PageModel
    {
        // A private field to hold the database context. This is used to interact with the database.
        private readonly AppDbContext _dbConnection;
        //Product Item bound to the form input
        [BindProperty]
        public ProductModel Item { get; set; }
        //Constructor injecting the database context
        public ProductEditModel(AppDbContext context)
        {
            //Assign the database context to _dbConnection
            _dbConnection = context;
        }
        //OnGet method that is called when the page is accessed
        public async Task<IActionResult> OnGetAsync(int id)
        {
            //Retrieve the Products from the Database based off of the ID given
            Item = await _dbConnection.Products.FindAsync(id);
            //If no Product is returned (likely due to being an incorrect ID)
            if (Item == null)
            {
                //Return NotFound() to the page
                return NotFound();
            }
            //Return to the page with the Product data
            return Page();
        }
        //Runs on submitting of the form
        public async Task<IActionResult> OnPostAsync()
        {
            //Retrieve the Products from the Database based off of the ID given
            var itemToUpdate = await _dbConnection.Products.FindAsync(Item.Guid);
            //If no Product is returned (likely due to being an incorrect ID)
            if (itemToUpdate == null)
            {
                //Return NotFound() to the page
                return NotFound();
            }
            // Update the properties of the item
            itemToUpdate.Name = Item.Name;
            itemToUpdate.Description = Item.Description;
            itemToUpdate.Price = Item.Price;
            itemToUpdate.Quantity = Item.Quantity;

            // Save the changes
            await _dbConnection.SaveChangesAsync();
            //Return to the Admin Page
            return RedirectToPage("Admin");
        }
    }
}
