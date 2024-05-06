using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoExampleAndy.Data;
using ToDoExampleAndy.Model;

namespace ToDoExampleAndy.Pages
{
    public class ProductCreateModel : PageModel
    {
        private readonly AppDbContext _dbConnection;

        public ProductModel Item { get; set; }

        public ProductCreateModel(AppDbContext context)
        {
            _dbConnection = context;
        }

        public void OnGet()
        {
            Item = new ProductModel(); // Initialize Item
        }

        public IActionResult OnPost(ProductModel Item)
        {
            //if (!ModelState.IsValid)
            //{
            // Log validation errors or set a breakpoint here to inspect ModelState
            //return Page();
            //}

            // Set a breakpoint here to inspect the 'Item' object
            _dbConnection.Products.Add(Item);
            _dbConnection.SaveChanges();

            return RedirectToPage("Admin");
        }
    }
}
