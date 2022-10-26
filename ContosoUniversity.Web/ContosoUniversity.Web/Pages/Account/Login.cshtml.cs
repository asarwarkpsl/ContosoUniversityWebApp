using ContosoUniversity.Data.Models.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ContosoUniversity.Web.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Users Credentials { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            //validation logic goes here

            if(Credentials != null)
            {
                if(Credentials.UserName.Equals("admin") && Credentials.Password.Equals("admin")) //dummy authenticated logic
                {

                    var claims = new List<Claim>{
                                        new Claim(ClaimTypes.Name, Credentials.UserName),
                                        new Claim(ClaimTypes.Email,"admin@unknown.com")
                                    };


                    ClaimsIdentity identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);          

                    return Redirect("/Index");
                }
            }

            return Page();
        }
    }
}
