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
            // Assign the database context instance to the private field _dbConnection
            _dbConnection = context;
        }
        //OnGet method that is called when the page is accessed
        public void OnGet(int id)
        {
            //Retrieve the Products from the Database connection and store them as a list
            Product = _dbConnection.Products.FirstOrDefault(t => t.Guid == id);
            //Try to run this code
            try
            {
                //Get the data from the User Claim as an array and store it as DataGet
                Claim[] DataGet = HttpContext.User.Claims.ToArray();
                //If the value from the Claim is null (user not logged in)
                if (DataGet[0].Value == null)
                {
                    //Redirect to the login page
                    Response.Redirect("/Login");
                    return;
                }
            }
            //If the code fails or causes an error (likely due to not being logged in so not returning data for the Claim) then run this instead
            catch
            {
                //Redirect to the Login Page
                Response.Redirect("/Login");
                return;
            }
            //If the Product returned is null (no data found, likely due to a false ID being entered)
            if (Product == null)
            {
                //Return to the Products Page
                Response.Redirect("/Products");
                return;
            }
        }

        //Runs on submitting of the form
        public IActionResult OnPost(int id, SalesModel Item, ProductModel Product, UserModel UserModel)
        {
            //Get the Product from the database by finding the first with the id
            Product = _dbConnection.Products.FirstOrDefault(t => t.Guid == id);
            //Set the TransactionDate of the Saleto the current DateTime
            Item.TransactionDate = DateTime.Now;
            //Set the ID of the Sale to the ID being viewed
            Item.ProductID = Product.Guid;
            //Set the Price of the Sale to the cost of a Product multiplied by the amount bought
            Item.Price = Item.Quantity * Product.Price;
            //Updates quantity of the Product after the purchase
            Product.Quantity = Product.Quantity - Item.Quantity;
            //Set the CustomerName in the Sale to the logged in users name
            Item.CustomerName = HttpContext.User.Identity.Name;
            //Add the new Sale entry to the database context
            _dbConnection.Sales.Add(Item);
            //Update the database with the Product change in quantity
            _dbConnection.Products.Update(Product);
            //Save all changes to the database
            _dbConnection.SaveChanges();
            //Redirect to the Products page
            return RedirectToPage("Products");
        }
    }
}
