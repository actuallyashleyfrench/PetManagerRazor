using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetManagerRazor.Data;
using PetManagerRazor.Models;

namespace PetManagerRazor.Pages.Owners
{
    /// <summary>
    /// PageModel for list of Owners with optional search filtering.
    /// </summary>
    public class IndexModel : PageModel
    {
        private readonly MyDbContext _context;

        public IndexModel(MyDbContext context) => _context = context;

        public List<Owner> Owners { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        /// <summary>
        /// Handles GET request to fetch Owners, including related Pets.
        /// Optionally filters Owners by SearchTerm.
        /// </summary>
        public async Task OnGetAsync()
        {
            var query = _context.Owners
                .Include(o => o.Pets)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                query = query.Where(o => o.Name.Contains(SearchTerm));
            }

            Owners = await query.ToListAsync();
        }
    }
}
