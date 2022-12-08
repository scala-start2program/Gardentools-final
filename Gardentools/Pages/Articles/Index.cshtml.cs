using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gardentools.Data;
using Gardentools.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Gardentools.Helpers;
using static System.Formats.Asn1.AsnWriter;
using System.Globalization;

namespace Gardentools.Pages.Articles
{
    public class IndexModel : PageModel
    {
        private readonly Gardentools.Data.GardentoolsContext _context;
        private readonly int ItemsPerPage = 6;

        public IndexModel(Gardentools.Data.GardentoolsContext context)
        {
            _context = context;
        }

        public IList<Article> Articles { get;set; } = default!;
        public List<SelectListItem> AvailableBrands { get; set; }
        public List<SelectListItem> AvailableCategories { get; set; }
        public int? SelectedBrandId { get; set; }
        public int? SelectedCategoryId { get; set; }
        public Pagination Pagination { get; private set; }
        public Availability Availability { get; set; }



        public void OnGet(int? pageIndex, int? brandFilter, int? categoryFilter)
        {
            Availability = new Availability(_context, HttpContext);
            SelectedBrandId = brandFilter;
            SelectedCategoryId = categoryFilter;
            PopulateCollections(brandFilter, categoryFilter, pageIndex);
        }
        public void OnPost(int? brandFilter, int? categoryFilter, int? pageIndex)
        {
            Availability = new Availability(_context, HttpContext);
            SelectedBrandId = brandFilter;
            SelectedCategoryId = categoryFilter;
            PopulateCollections(brandFilter, categoryFilter, pageIndex);
        }



        private void PopulateCollections(int? brandFilter, int? categoryFilter, int? pageIndex)
        {
            int articleCount = 0;
            if (brandFilter == null && categoryFilter == null)
                articleCount = _context.Article.Count();
            else if (brandFilter != null && categoryFilter == null)
                articleCount = _context.Article.Count(b => b.BrandId.Equals(brandFilter));
            else if (brandFilter == null && categoryFilter != null)
                articleCount = _context.Article.Count(f => f.CategoryId.Equals(categoryFilter));
            else
                articleCount = _context.Article.Count(s => s.BrandId.Equals(brandFilter) && s.CategoryId.Equals(categoryFilter));

            Pagination = new Pagination(articleCount, pageIndex, ItemsPerPage);

            IQueryable<Article> query;
            if (brandFilter == null && categoryFilter == null)
            {
                query = _context.Article
                    .Include(b => b.Brand)
                    .Include(f => f.Category)
                    .OrderBy(b => b.Brand.BrandName)
                    .ThenBy(f => f.Category.CategoryName);
                int regels = query.Count();
            }
            else if (brandFilter != null && categoryFilter == null)
            {
                query = _context.Article
                    .Where(b => b.BrandId.Equals(brandFilter))
                    .Include(b => b.Brand)
                    .Include(f => f.Category)
                    .OrderBy(b => b.Brand.BrandName)
                    .ThenBy(f => f.Category.CategoryName);
            }
            else if (brandFilter == null && categoryFilter != null)
            {
                query = _context.Article
                    .Where(b => b.CategoryId.Equals(categoryFilter))
                    .Include(b => b.Brand)
                    .Include(f => f.Category)
                    .OrderBy(b => b.Brand.BrandName)
                    .ThenBy(f => f.Category.CategoryName);
            }
            else
            {
                query = _context.Article
                    .Where(b => b.BrandId.Equals(brandFilter) && b.CategoryId.Equals(categoryFilter))
                    .Include(b => b.Brand)
                    .Include(f => f.Category)
                    .OrderBy(b => b.Brand.BrandName)
                    .ThenBy(f => f.Category.CategoryName);
            }
            Articles = query
                .Skip(Pagination.FirstObjectIndex)
                .Take(ItemsPerPage)
                .ToList();

            AvailableBrands = _context.Brand.Select(a =>
                new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.BrandName
                }).OrderBy(b => b.Text).ToList();
            AvailableCategories = _context.Category.Select(a =>
                new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.CategoryName
                }).OrderBy(b => b.Text).ToList();

            AvailableBrands.Insert(0, new SelectListItem()
            {
                Value = null,
                Text = "--- Alle merken ---"
            });
            AvailableCategories.Insert(0, new SelectListItem()
            {
                Value = null,
                Text = "--- Alle categoriën ---"
            });
        }

    }
}
