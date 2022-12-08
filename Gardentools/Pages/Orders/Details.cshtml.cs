using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gardentools.Data;
using Gardentools.Models;
using Gardentools.Helpers;

namespace Gardentools.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly Gardentools.Data.GardentoolsContext _context;

        public DetailsModel(Gardentools.Data.GardentoolsContext context)
        {
            _context = context;
        }
        public Availability Availability { get; set; }

        public Order Order { get; set; }
        public IList<OrderDetail> OrderDetail { get; set; }

        public IActionResult OnGet(int? id)
        {
            Availability = new Availability(_context, HttpContext);
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = _context.Order
                .Include(b => b.User)
                .FirstOrDefault(m => m.Id == id);
            var orderDetail = _context.OrderDetail
                .Include(od => od.Article)
                .Include(od => od.Article.Brand)
                .Where(od => od.OrderId.Equals(id));

            if (order == null)
            {
                return NotFound();
            }
            else 
            {
                Order = order;
                OrderDetail = orderDetail.ToList();
            }
            return Page();
        }
    }
}
