using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetManagerRazor.Data;
using PetManagerRazor.Models;

namespace PetManagerRazor.Pages.Pets
{
    /// <summary>
    /// PageModel for listing and searching Pets.
    /// </summary>
    public class IndexModel : PageModel
    {
        private readonly MyDbContext _context;

        public IndexModel(MyDbContext context) => _context = context;

        public List<Pet> Pets { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        /// <summary>
        /// Handles GET request to load Pets including their Owners. 
        /// Optionally filters Pets by SearchTerm.
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync()
        {
            var query = _context.Pets
                .Include(p => p.Owner)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                query = query.Where(p => p.Name.Contains(SearchTerm));
            }

            Pets = await query.ToListAsync();
        }
    }
}
