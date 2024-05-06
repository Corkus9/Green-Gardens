using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using ToDoExampleAndy.Data;
using ToDoExampleAndy.Model;
using Microsoft.IdentityModel.Tokens;

namespace ToDoExampleAndy.Pages
{
    public class LayoutModel : PageModel
    {
        // A private field to hold the database context. This is used to interact with the database
        private readonly AppDbContext _db;
        //OnGet method that is called when the page is accessed.
        public void OnGet()
        {
            //Try to run this code
            try
            {
                //Get the data from the User Claim as an array and store it as DataGet
                Claim[] DataGet = HttpContext.User.Claims.ToArray();
                //If the Role value from the Claim is not "Admin"
                if (DataGet[3].Value != "Admin")
                {
                    //Redirect to the Page
                    Response.Redirect("/");
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


        }
    }
}