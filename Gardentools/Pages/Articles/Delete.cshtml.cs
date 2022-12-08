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

namespace Gardentools.Pages.Articles
{
    public class DeleteModel : PageModel
    {
        private readonly Gardentools.Data.GardentoolsContext _context;

        public DeleteModel(Gardentools.Data.GardentoolsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Article Article { get; set; }
        public Availability Availability { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Availability = new Availability(_context, HttpContext);
            if (!Availability.IsAdmin)
            {
                return RedirectToPage("../Articles/Index");
            }
            if (id == null || _context.Article == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .Include(b => b.Brand)
                .Include(f => f.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            else
            {
                Article = article;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            Availability = new Availability(_context, HttpContext);
            if (!Availability.IsAdmin)
            {
                return RedirectToPage("../articles/Index");
            }
            if (id == null || _context.Article == null)
            {
                return NotFound();
            }
            var article = await _context.Article.FindAsync(id);

            if (article != null)
            {
                Article = article;
                _context.Article.Remove(Article);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
