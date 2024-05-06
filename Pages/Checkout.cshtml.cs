using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Principal;
using ToDoExampleAndy.Data;
using ToDoExampleAndy.Model;
using System.Web;

namespace ToDoExampleAndy.Pages
{
    public class CheckoutModel : PageModel
    {
        // A private field to hold the database context. This is used to interact with the database.
        private readonly AppDbContext _dbConnection;
        //A variable of the type SalesModel to store the retrieved data
        public SalesModel Item { get; set; }
        //A variable of the type ProductModel to store the retrieved data
        public ProductModel Product { get; set; }
        //A variable of the type UserModel to store the retrieved data
        public UserModel UserModel { get; set; }
        //A function to set the _dbConnection to the database context
        public CheckoutModel(AppDbContext context)
        {
            _dbConnection = context;
        }
        //OnGet method that is called when the page is accessed
        public void OnGet(int id)
        {
            Product = _dbConnection.Products.FirstOrDefault(t => t.Guid == id);

            try
            {
                Claim[] DataGet = HttpContext.User.Claims.ToArray();
                if (DataGet[0].Value == null)
                {
                    Response.Redirect("/Login");
                    return;
                }
            }
            catch
            {
                Response.Redirect("/Login");
                return;
            }

            if (Product == null)
            {
                Response.Redirect("/Products");
                return;
            }
        }


        public IActionResult OnPost(int id, SalesModel Item, ProductModel Product, UserModel UserModel)
        {
            //if (!ModelState.IsValid)
            //{
            // Log validation errors or set a breakpoint here to inspect ModelState
            //return Page();
            //}

            // Set a breakpoint here to inspect the 'Item' object


            Product = _dbConnection.Products.FirstOrDefault(t => t.Guid == id);

            Item.TransactionDate = DateTime.Now;
            Item.ProductID = Product.Guid;
            Item.Price = Item.Quantity * Product.Price;
            //Updates quantity after purchase
            Product.Quantity = Product.Quantity - Item.Quantity;
            Item.CustomerName = HttpContext.User.Identity.Name;

            _dbConnection.Sales.Add(Item);
            _dbConnection.Products.Update(Product);
            _dbConnection.SaveChanges();

            return RedirectToPage("Products");
        }
    }
}
