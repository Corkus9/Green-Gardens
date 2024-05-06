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
        private readonly AppDbContext _dbConnection;

        public ProductModel Item { get; set; }

        public ProductDeleteModel(AppDbContext context)
        {
            _dbConnection = context;
        }

        public void OnGet(int id)
        {
            // Retrieve the item to be deleted
            Item = _dbConnection.Products.FirstOrDefault(t => t.Guid == id);
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var itemToDelete = _dbConnection.Products.FirstOrDefault(t => t.Guid == id);
            if (itemToDelete != null)
            {
                _dbConnection.Products.Remove(itemToDelete);
                await _dbConnection.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
                // Handle the case where the item does not exist
                return NotFound();
            }
        }
    }
}
