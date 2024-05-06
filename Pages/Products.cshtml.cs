using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoExampleAndy.Data;
using ToDoExampleAndy.Model;

//Add-Migration AddProductTable
//Update-Database
namespace ToDoExampleAndy.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly AppDbContext _db;

        public List<ProductModel> Products { get; set; }

        public ProductsModel (AppDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            Products = _db.Products.ToList();
        }
    }
}
