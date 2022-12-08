using Gardentools.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gardentools.Pages.Users
{
    public class RegisterModel : PageModel
    {
        private readonly Gardentools.Data.GardentoolsContext _context;

        public RegisterModel(Gardentools.Data.GardentoolsContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User User { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            User.Password = Gardentools.Helpers.Encoding.EncodePassword(User.Password);
            _context.User.Add(User);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return Page();
            }
            string IdCookie = Gardentools.Helpers.Encoding.EncryptString(User.Id.ToString(), "P@sw00rd");
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddDays(365));
            HttpContext.Response.Cookies.Append("UserId", IdCookie, cookieOptions);
            return RedirectToPage("../Articles/Index");

        }

    }
}
