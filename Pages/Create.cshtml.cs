using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoExampleAndy.Data;
using ToDoExampleAndy.Model;

namespace ToDoExampleAndy.Pages
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _dbConnection;

        public TaskModel Item { get; set; }

        public CreateModel(AppDbContext context)
        {
            _dbConnection = context;
        }

        public void OnGet()
        {
            Item = new TaskModel(); // Initialize Item
        }

        public IActionResult OnPost(TaskModel Item)
        {
            if (!ModelState.IsValid)
            {
                // Log validation errors or set a breakpoint here to inspect ModelState
                return Page();
            }

            // Set a breakpoint here to inspect the 'Item' object
            _dbConnection.Tasks.Add(Item);
            _dbConnection.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
