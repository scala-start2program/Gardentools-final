using Gardentools.Data;
using Gardentools.Models;

namespace Gardentools.Helpers
{
    public class Availability
    {
        public string UserId { get; } = "";
        public bool IsAdmin { get; } = false;
        public string Email { get; } = "";
        public string ConfigButtonStyle { get; } = "visibility:hidden;";
        public string BasketCount { get; } = "";
        public Availability(GardentoolsContext context, HttpContext httpContext)
        {
            string userId = httpContext.Request.Cookies["UserID"];
            if (!string.IsNullOrEmpty(userId))
            {
                userId = Encoding.DecryptString(userId, "P@sw00rd");
                User user = context.User.FirstOrDefault(m => m.Id == int.Parse(userId));
                if (user != null)
                {
                    UserId = userId;
                    IsAdmin = user.IsAdmin;
                    Email = user.Email;
                    if (IsAdmin)
                    {
                        ConfigButtonStyle = "visibility:visible;";
                    }
                    int findId = int.Parse(userId);
                    BasketCount = context.Basket.Where(b => b.UserId == findId).Count().ToString();
                }
            }

        }

    }
}
