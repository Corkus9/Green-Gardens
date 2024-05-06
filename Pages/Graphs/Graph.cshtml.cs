using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using ToDoExampleAndy.Data;
using ToDoExampleAndy.Model;


namespace ToDoExampleAndy.Pages.Graphs
{
    public class GraphModel : PageModel
    {

        private readonly AppDbContext _dbConnection;

        public string TasksJson { get; set; }

        public GraphModel(AppDbContext db)
        {
            _dbConnection = db;
        }
        

        public void OnGet()
        {
            var items = _dbConnection.Tasks.ToList();
            TasksJson = JsonSerializer.Serialize(items.Select(t => new { DueDate = t.DueDate.ToString("yyyy-MM-dd"), t.Completed }));

        }
    }
}
