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
    public class DeleteModel : PageModel
    {
        private readonly Gardentools.Data.GardentoolsContext _context;

        public DeleteModel(Gardentools.Data.GardentoolsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Brand Brand { get; set; }
        public Availability Availability { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Availability = new Availability(_context, HttpContext);
            if (!Availability.IsAdmin)
            {
                return RedirectToPage("../Articles/Index");
            }
            if (id == null || _context.Brand == null)
            {
                return NotFound();
            }

            var brand = await _context.Brand.FirstOrDefaultAsync(m => m.Id == id);

            if (brand == null)
            {
                return NotFound();
            }
            else
            {
                Brand = brand;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            Availability = new Availability(_context, HttpContext);
            if (!Availability.IsAdmin)
            {
                return RedirectToPage("../Articles/Index");
            }
            if (id == null || _context.Brand == null)
            {
                return NotFound();
            }
            var brand = await _context.Brand.FindAsync(id);

            if (brand != null)
            {
                Brand = brand;
                _context.Brand.Remove(Brand);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
