using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoExampleAndy.Data;
using ToDoExampleAndy.Model;

namespace ToDoExampleAndy.Pages
{
    public class IndexModel : PageModel
    {
        // A private field for logging. ILogger is used for logging different types of information.
        private readonly ILogger<IndexModel> _logger;

        // Public property for Username. This can be accessed and modified from outside the class.
        public string Username { get; set; }

        // Public property for a list of TaskModel items. This stores the list of tasks.
        public List<TaskModel> Items { get; set; }

        // A private field to hold the database context. This is used to interact with the database.
        private readonly AppDbContext _dbConnection;

        // Constructor for the IndexModel class.
        // It takes an ILogger and an instance of AppDbContext as parameters.
        public IndexModel(ILogger<IndexModel> logger, AppDbContext _db)
        {
            // Assign the logger instance to the private field _logger.
            _logger = logger;

            // Assign the database context instance to the private field _dbConnection.
            _dbConnection = _db;
        }

        // OnGet method that is called when the page is accessed.
        public void OnGet()
        {
            // Retrieve all tasks from the database and assign them to the Items property.
            Items = _dbConnection.Tasks.ToList();

            // Set the Username property to "DefaultUser".
            // This could be replaced with dynamic data in a real application.
            Username = "DefaultUser";
        }
    }
}