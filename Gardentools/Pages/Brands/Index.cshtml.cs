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

namespace Gardentools.Pages.Brands
{
    public class IndexModel : PageModel
    {
        private readonly Gardentools.Data.GardentoolsContext _context;

        public IndexModel(Gardentools.Data.GardentoolsContext context)
        {
            _context = context;
        }
        public Availability Availability { get; set; }
        public IList<Models.Brand> Brands { get; set; } = default!;
        public Pagination Pagination { get; private set; }
        public void OnGet(int? pageIndex)
        {
            Availability = new Availability(_context, HttpContext);
            if (!Availability.IsAdmin)
            {
                Response.Redirect("../Articles/Index");
            }
            Pagination = new Pagination(_context.Brand.Count(), pageIndex, 4);

            IQueryable<Brand> query = _context.Brand
                .OrderBy(f => f.BrandName);
            Brands = query
                    .Skip(Pagination.FirstObjectIndex)
                    .Take(4).ToList();

        }


    }
}
