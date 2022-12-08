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

namespace Gardentools.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Gardentools.Data.GardentoolsContext _context;

        public IndexModel(Gardentools.Data.GardentoolsContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;
        public Availability Availability { get; set; }
        public Pagination Pagination { get; private set; }
        public void OnGet(int? pageIndex)
        {
            Availability = new Availability(_context, HttpContext);
            if (!Availability.IsAdmin)
            {
                Response.Redirect("../Articles/Index");
            }
            Pagination = new Pagination(_context.Category.Count(), pageIndex, 10);
            IQueryable<Category> query = _context.Category
                .OrderBy(f => f.CategoryName);
            Category = query
                .Skip(Pagination.FirstObjectIndex)
                .Take(10).ToList();
        }
    }
}
