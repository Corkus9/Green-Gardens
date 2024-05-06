using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Text.Json;
using ToDoExampleAndy.Data;
using ToDoExampleAndy.Model;



namespace ToDoExampleAndy.Pages.Graphs
{
    public class GraphModel : PageModel
    {

        private readonly AppDbContext _dbConnection;

        public List<SalesModel> Sales { get; set; }

        public string SalesJson { get; set; }

        public GraphModel(AppDbContext db)
        {
            _dbConnection = db;
        }


        public void OnGet()
        {
            Sales = _dbConnection.Sales.ToList();
            SalesJson = JsonSerializer.Serialize(Sales.Select(t => new { TransactionDate = t.TransactionDate.ToString("MM-dd-yyyy") }));

            try
            {
                Claim[] DataGet = HttpContext.User.Claims.ToArray();
                if (DataGet[3].Value != "Admin")
                {
                    Response.Redirect("/Products");
                    return;
                }

            }
            catch
            {
                Response.Redirect("/Privacy");
                return;
            }
        }
    }
}
