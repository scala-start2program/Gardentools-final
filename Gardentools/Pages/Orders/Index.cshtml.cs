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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gardentools.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly Gardentools.Data.GardentoolsContext _context;

        public IndexModel(Gardentools.Data.GardentoolsContext context)
        {
            _context = context;
        }
        public Availability Availability { get; set; }
        public Pagination Pagination { get; private set; }
        public List<SelectListItem> AvailableUsers { get; set; }
        public int? SelectedUserId { get; set; }

        public IList<Order> Order { get; set; } = default!;

        public void OnGet(int? pageIndex)
        {
            Availability = new Availability(_context, HttpContext);
            PopulateCollections(null, pageIndex);

        }
        public void OnPost(int? userFilter, int? pageIndex)
        {
            Availability = new Availability(_context, HttpContext);
            SelectedUserId = userFilter;
            PopulateCollections(userFilter, pageIndex);
        }

        private void PopulateCollections(int? userFilter, int? pageIndex)
        {
            int orderCount = 0;
            if (userFilter != null)
                orderCount = _context.Order.Count(b => b.UserId.Equals(userFilter));
            else
                orderCount = _context.Order.Count();
            Pagination = new Pagination(orderCount, pageIndex, 10);
            IQueryable<Order> query;
            if (userFilter == null)
            {
                query = _context.Order
                    .Include(b => b.User)
                    .OrderByDescending(f => f.DateTimeStamp);
            }
            else
            {
                query = _context.Order
                    .Where(b => b.UserId.Equals(userFilter))
                    .Include(b => b.User)
                    .OrderByDescending(f => f.DateTimeStamp);
            }
            Order = query
                .Skip(Pagination.FirstObjectIndex)
                .Take(10)
                .ToList();

            AvailableUsers = _context.User
                .Select(a =>
                new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name + " " + a.FirstName
                }).OrderBy(b => b.Text).ToList();

            AvailableUsers.Insert(0, new SelectListItem()
            {
                Value = null,
                Text = "--- Alle klanten ---"
            });
        }
    }
}
