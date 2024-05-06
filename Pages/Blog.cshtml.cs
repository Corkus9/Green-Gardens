using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ToDoExampleAndy.Pages
{
    public class BlogModel : PageModel
    {
        // A private field for logging. ILogger is used for logging different types of information
        private readonly ILogger<BlogModel> _logger;

        // Constructor for the IndexModel class
        // It takes an ILogger and an instance of AppDbContext as parameters
        public BlogModel(ILogger<BlogModel> logger)
        {
            // Assign the logger instance to the private field _logger
            _logger = logger;
        }

        // OnGet method that is called when the page is accessed
        public void OnGet()
        {
        }
    }
}