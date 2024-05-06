using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoExampleAndy.Data;
using ToDoExampleAndy.Model;
using System.Threading.Tasks;
using System.Linq;

namespace ToDoExampleAndy.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _dbConnection;

        public TaskModel Item { get; set; }

        public DeleteModel(AppDbContext context)
        {
            _dbConnection = context;
        }

        public void OnGet(int id)
        {
            // Retrieve the item to be deleted
            Item = _dbConnection.Tasks.FirstOrDefault(t => t.Id == id);
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var itemToDelete = _dbConnection.Tasks.FirstOrDefault(t => t.Id == id);
            if (itemToDelete != null)
            {
                _dbConnection.Tasks.Remove(itemToDelete);
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
