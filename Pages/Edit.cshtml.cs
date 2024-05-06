using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoExampleAndy.Data;
using ToDoExampleAndy.Model;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoExampleAndy.Pages
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _dbConnection;

        [BindProperty]
        public TaskModel Item { get; set; }

        public EditModel(AppDbContext context)
        {
            _dbConnection = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Item = await _dbConnection.Tasks.FindAsync(id);

            if (Item == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var itemToUpdate = await _dbConnection.Tasks.FindAsync(Item.Id);

            if (itemToUpdate == null)
            {
                return NotFound();
            }

            // Update the properties of the item
            itemToUpdate.Description = Item.Description;
            itemToUpdate.Completed = Item.Completed;
            itemToUpdate.DueDate = Item.DueDate;

            // Save the changes
            await _dbConnection.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
