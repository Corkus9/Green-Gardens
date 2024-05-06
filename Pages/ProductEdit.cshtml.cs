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
        private readonly AppDbContext _dbConnection;

        [BindProperty]
        public ProductModel Item { get; set; }

        public ProductEditModel(AppDbContext context)
        {
            _dbConnection = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Item = await _dbConnection.Products.FindAsync(id);

            if (Item == null)
            {

                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //return RedirectToPage("Products");
            //return Page();
            //}

            var itemToUpdate = await _dbConnection.Products.FindAsync(Item.Guid);

            if (itemToUpdate == null)
            {

                return NotFound();
            }

            // Update the properties of the item
            itemToUpdate.Name = Item.Name;
            itemToUpdate.Description = Item.Description;
            itemToUpdate.Price = Item.Price;
            itemToUpdate.Quantity = Item.Quantity;

            // Save the changes
            await _dbConnection.SaveChangesAsync();

            return RedirectToPage("Admin");
        }
    }
}
