using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using ToDoExampleAndy.Data;
using ToDoExampleAndy.Model;

//Add-Migration AddProductTable
//Update-Database
namespace ToDoExampleAndy.Pages
{
    public class ProductsModel : PageModel
    {
        // A private field to hold the database context. This is used to interact with the database.
        private readonly AppDbContext _db;
        //A list of all returned Products from the database
        public List<ProductModel> Products { get; set; }
        //Constructor injecting the database context
        public ProductsModel(AppDbContext db)
        {
            //Assign the database context to _db
            _db = db;
        }
        //OnGet method that is called when the page is accessed
        public void OnGet()
        {
            //Retrieve the Products from the Database connection and store them as a list
            Products = _db.Products.ToList();
        }
    }
}
