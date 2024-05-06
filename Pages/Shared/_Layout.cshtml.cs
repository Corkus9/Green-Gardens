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
        private readonly AppDbContext _db;

        public void OnGet()
        {
            try
            {
                Claim[] DataGet = HttpContext.User.Claims.ToArray();
                if (DataGet[3].Value != "Admin")
                {
                    Response.Redirect("/");
                    return;
                }

                ViewData["FName"] = DataGet[0].Value;
                ViewData["Admin"] = DataGet[3].Value;

            }
            catch
            {
                Response.Redirect("/Login");
                return;
            }


        }
    }
}