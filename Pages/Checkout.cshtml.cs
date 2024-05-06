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
        private readonly AppDbContext _dbConnection;

        public Guid UserID { get; set; }

        public SalesModel Item { get; set; }

        public ProductModel Product { get; set; }

        public UserModel UserModel { get; set; }

        public CheckoutModel(AppDbContext context)
        {
            _dbConnection = context;
        }

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
