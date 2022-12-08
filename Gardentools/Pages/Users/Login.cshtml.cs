using Gardentools.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gardentools.Pages.Users
{
    public class LoginModel : PageModel
    {
        private readonly Gardentools.Data.GardentoolsContext _context;

        public LoginModel(Gardentools.Data.GardentoolsContext context)
        {
            _context = context;
        }
        public string Email { get; set; }
        public string Password { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }
        public IActionResult OnPost(string email, string password)
        {
            Email = email;
            Password = password;
            string encPassword = Gardentools.Helpers.Encoding.EncodePassword(password);
            User user = _context.User.FirstOrDefault(u => u.Email == email && u.Password == encPassword);
            if (user == null)
            {
                return Page();
            }
            else
            {
                string IdCookie = Gardentools.Helpers.Encoding.EncryptString(user.Id.ToString(), "P@sw00rd");
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddDays(7));
                HttpContext.Response.Cookies.Append("UserId", IdCookie, cookieOptions);
                return RedirectToPage("../Articles/Index");
            }
        }

    }
}
