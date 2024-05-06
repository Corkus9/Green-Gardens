using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoExampleAndy.Data;
using ToDoExampleAndy.Model;

namespace ToDoExampleAndy.Pages
{
    public class SaleCreateModel : PageModel
    {
        private readonly AppDbContext _dbConnection;

        public SalesModel Item { get; set; }

        public SaleCreateModel(AppDbContext context)
        {
            _dbConnection = context;
        }
        //OnGet method that is called when the page is accessed
        public void OnGet()
        {
            Item = new SalesModel();
        }

        public IActionResult OnPost(SalesModel Item)
        {
            //if (!ModelState.IsValid)
            //{
            // Log validation errors or set a breakpoint here to inspect ModelState
            //return Page();
            //}

            // Set a breakpoint here to inspect the 'Item' object

            Item.TransactionDate = DateTime.Now;

            _dbConnection.Sales.Add(Item);
            _dbConnection.SaveChanges();

            return RedirectToPage("Admin");
        }
    }
}
