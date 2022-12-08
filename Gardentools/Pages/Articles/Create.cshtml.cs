using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Gardentools.Data;
using Gardentools.Models;
using Gardentools.Helpers;
using static System.Formats.Asn1.AsnWriter;

namespace Gardentools.Pages.Articles
{
    public class CreateModel : PageModel
    {
        private readonly Gardentools.Data.GardentoolsContext _context;
        private readonly IWebHostEnvironment webhostEnvironment;
        public CreateModel(Gardentools.Data.GardentoolsContext context,
            IWebHostEnvironment webhostEnvironment)
        {
            _context = context;
            this.webhostEnvironment = webhostEnvironment;
        }

        public IActionResult OnGet()
        {
            Availability = new Availability(_context, HttpContext);
            if (!Availability.IsAdmin)
            {
                return RedirectToPage("../articles/Index");
            }
            ViewData["BrandId"] = new SelectList(_context.Brand.OrderBy(b => b.BrandName).ToList(), "Id", "BrandName");
            ViewData["CategoryId"] = new SelectList(_context.Category.OrderBy(b => b.CategoryName).ToList(), "Id", "CategoryName");
            return Page();
        }

        [BindProperty]
        public Article Article { get; set; }
        [BindProperty]
        public IFormFile PhotoUpload { get; set; }
        public Availability Availability { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Availability = new Availability(_context, HttpContext);
            if (!Availability.IsAdmin)
            {
                return RedirectToPage("../articles/Index");
            }
            ViewData["BrandId"] = new SelectList(_context.Brand.OrderBy(b => b.BrandName).ToList(), "Id", "BrandName");
            ViewData["CategoryId"] = new SelectList(_context.Category.OrderBy(b => b.CategoryName).ToList(), "Id", "CategoryName");

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
            _context.Article.Add(Article);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
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
