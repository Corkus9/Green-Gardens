using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ToDoExampleAndy.Pages
{
    public class LogoutModel : PageModel
    {
        //OnGet method that is called when the page is accessed.
        public async Task<IActionResult> OnGetAsync()
        {
            //Run the HttpContext SignOutAsync function
            await HttpContext.SignOutAsync();
            //Redirec to the home page
            return RedirectToPage("/Index");
        }
    }
}
