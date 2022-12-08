using Gardentools.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gardentools.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Gardentools.Data.GardentoolsContext _context;
        public IndexModel(ILogger<IndexModel> logger,
            Gardentools.Data.GardentoolsContext context
)
        {
            _logger = logger;
            _context = context;
        }
        public Availability Availability { get; set; }
        public void OnGet()
        {
            Availability = new Availability(_context, HttpContext);
        }
    }
}