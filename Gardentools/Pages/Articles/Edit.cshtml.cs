using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gardentools.Data;
using Gardentools.Models;
using Gardentools.Helpers;
using static System.Formats.Asn1.AsnWriter;

namespace Gardentools.Pages.Articles
{
    public class EditModel : PageModel
    {
        private readonly Gardentools.Data.GardentoolsContext _context;
        private readonly IWebHostEnvironment webhostEnvironment;

        public EditModel(Gardentools.Data.GardentoolsContext context,
            IWebHostEnvironment webhostEnvironment)
        {
            _context = context;
            this.webhostEnvironment = webhostEnvironment;
        }

        [BindProperty]
        public Article Article { get; set; } = default!;
        [BindProperty]
        public IFormFile PhotoUpload { get; set; }
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

            var article = await _context.Article.FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            Article = article;
            ViewData["BrandId"] = new SelectList(_context.Brand.OrderBy(b => b.BrandName).ToList(), "Id", "BrandName");
            ViewData["CategoryId"] = new SelectList(_context.Category.OrderBy(f => f.CategoryName).ToList(), "Id", "CategoryName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Availability = new Availability(_context, HttpContext);
            if (!Availability.IsAdmin)
            {
                return RedirectToPage("../articles/Index");
            }
            ViewData["BrandId"] = new SelectList(_context.Brand.OrderBy(b => b.BrandName).ToList(), "Id", "BrandName");
            ViewData["CategoryId"] = new SelectList(_context.Category.OrderBy(f => f.CategoryName).ToList(), "Id", "CategoryName");

            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (PhotoUpload != null)
            {
                if (Article.ImagePath != null)
                {
                    string filePath = Path.Combine(webhostEnvironment.WebRootPath, "images", Article.ImagePath);
                    System.IO.File.Delete(filePath);
                }
                Article.ImagePath = ProcessUploadedFile();
            }
            _context.Attach(Article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(Article.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ArticleExists(int id)
        {
            return _context.Article.Any(e => e.Id == id);
        }
        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (PhotoUpload != null)
            {
                string uploadFolder = Path.Combine(webhostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + PhotoUpload.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    PhotoUpload.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
