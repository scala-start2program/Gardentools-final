using Gardentools.Helpers;
using Gardentools.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Gardentools.Pages.Users
{
    public class AccountModel : PageModel
    {
        private readonly Gardentools.Data.GardentoolsContext _context;

        public AccountModel(Gardentools.Data.GardentoolsContext context)
        {
            _context = context;
        }
        public User Me { get; set; }
        public Availability Availability { get; set; }

        public List<SelectListItem> AvailableOrders { get; set; }
        public IList<OrderDetail> OrderDetail { get; set; } = default!;

        public int? SelectedOrderId { get; set; }
        public IActionResult OnGet()
        {
            Availability = new Availability(_context, HttpContext);
            if (string.IsNullOrEmpty(Availability.UserId))
            {
                return NotFound();
            }
            int id = int.Parse(Availability.UserId);

            Me = _context.User.FirstOrDefault(m => m.Id == id);
            if (Me == null)
            {
                return NotFound();
            }
            Me.Password = "";


            AvailableOrders = _context.Order
                .Where(o=>o.UserId == Me.Id)
                .Select(a =>
                new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.DateTimeStamp.ToString()
                }).OrderBy(b => b.Text).ToList();

            int? orderId = null;
            var firstOrder = _context.Order
                .Where(o => o.UserId == Me.Id)
                .FirstOrDefault();
            if (firstOrder != null)
                orderId = firstOrder.Id;


            OrderDetail = _context.OrderDetail
                .Include(od => od.Article)
                .Include(od => od.Article.Brand)
                .Include(od => od.Article.Category)
                .Where(od => od.OrderId.Equals(orderId))
                .ToList();


            return Page();

        }
        public IActionResult OnPost(int? save, int? orderFilter)
        {
            if (save != null)
            {
                int searchId = Me.Id;
                User copyUser = _context.User.FirstOrDefault(m => m.Id == searchId);
                bool isAdmin = copyUser.IsAdmin;
                string oldPassword = copyUser.Password;


                if (Me.Password != null && Me.Password.Length > 0)
                    Me.Password = Gardentools.Helpers.Encoding.EncodePassword(Me.Password);
                else
                    Me.Password = oldPassword;
                Me.IsAdmin = isAdmin;
                _context.Attach(copyUser).State = EntityState.Detached;

                _context.Attach(Me).State = EntityState.Modified;
                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(Me.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToPage("../articles/Index");
            }
            else
            {
                Availability = new Availability(_context, HttpContext);
                if (string.IsNullOrEmpty(Availability.UserId))
                {
                    return NotFound();
                }
                int id = int.Parse(Availability.UserId);

                Me = _context.User.FirstOrDefault(m => m.Id == id);
                if (Me == null)
                {
                    return NotFound();
                }
                Me.Password = "";


                AvailableOrders = _context.Order
                    .Where(o => o.UserId == Me.Id)
                    .Select(a =>
                    new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.DateTimeStamp.ToString()
                    }).OrderBy(b => b.Text).ToList();

                if(orderFilter == null)
                {
                    var firstOrder = _context.Order
                        .Where(o => o.UserId == Me.Id)
                        .FirstOrDefault();
                    if (firstOrder != null)
                        orderFilter = firstOrder.Id;
                }

                OrderDetail = _context.OrderDetail
                    .Include(od => od.Article)
                    .Include(od => od.Article.Category)
                    .Include(od => od.Article.Brand)
                    .Where(od => od.OrderId.Equals(orderFilter))
                    .ToList();

                SelectedOrderId = orderFilter;

                return Page();
            }
        }
        private void PopulateCollections(int? id, int? orderFilter)
        {
        }
        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
