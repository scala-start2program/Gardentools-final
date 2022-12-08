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

namespace Gardentools.Pages.Baskets
{
    public class IndexModel : PageModel
    {
        private readonly Gardentools.Data.GardentoolsContext _context;

        public IndexModel(Gardentools.Data.GardentoolsContext context)
        {
            _context = context;
        }

        public IList<Basket> Basket { get; set; } = default!;
        public Availability Availability { get; set; }
        public ActionResult OnGet()
        {
            int userId;
            Availability = new Availability(_context, HttpContext);
            if (string.IsNullOrEmpty(Availability.UserId))
            {
                return RedirectToPage("../Users/Login");
            }
            else
            {
                userId = int.Parse(Availability.UserId);
            }

            IQueryable<Basket> query = _context.Basket
                .Include(b => b.Article)
                .Include(b => b.Article.Brand)
                .Include(f => f.Article.Category)
                .Include(b => b.User);
            query = query.Where(b => b.UserId.Equals(userId));
            Basket = query.ToList();
            if (Basket == null)
            {
                return NotFound();
            }
            return Page();
        }
        public ActionResult OnPost(string basketId, string newAmount, int? delete)
        {
            int userId;
            Availability = new Availability(_context, HttpContext);
            if (string.IsNullOrEmpty(Availability.UserId))
            {
                return RedirectToPage("../Users/Login");
            }
            else
            {
                userId = int.Parse(Availability.UserId);
            }
            if (delete != null)
            {
                newAmount = "0";
            }
            if (newAmount != null && basketId != null)
            {
                int amount = 0;
                int id = 0;
                try
                {
                    amount = int.Parse(newAmount);
                    id = int.Parse(basketId);
                }
                catch
                {
                    return Page();
                }
                if (amount == 0)
                {
                    Basket basket = GetBasket(id);
                    if (basket != null)
                    {
                        _context.Basket.Remove(basket);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    Basket basket = GetBasket(id);
                    basket.Count = amount;
                    _context.Attach(basket).State = EntityState.Modified;
                    try
                    {
                        _context.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!BasketExists(basket.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }


            IQueryable<Basket> query = _context.Basket
                .Include(b => b.Article)
                .Include(b => b.Article.Brand)
                .Include(f => f.Article.Category)
                .Include(b => b.User);
            query = query.Where(b => b.UserId.Equals(userId));
            Basket = query.ToList();
            if (Basket == null)
            {
                return NotFound();
            }
            return Page();
        }

        private bool BasketExists(int id)
        {
            return _context.Basket.Any(e => e.Id == id);
        }

        private Basket GetBasket(int id)
        {
            Basket basket = _context.Basket.FirstOrDefault(b => b.Id == id);
            return basket;
        }

    }
}
