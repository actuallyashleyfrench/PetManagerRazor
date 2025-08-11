using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetManagerRazor.Data;
using PetManagerRazor.Models;

namespace PetManagerRazor.Pages.Owners
{
    /// <summary>
    /// PageModel for displaying Owner details.
    /// </summary>
    public class DetailsModel : PageModel
    {
        private readonly MyDbContext _context;

        public DetailsModel(MyDbContext context) => _context = context;

        public Owner? Owner { get; set; }

        /// <summary>
        /// Handles GET request to retrieve an Owner by id, including their Pets.
        /// </summary>
        /// <param name="id">Owner's id</param>
        /// <returns>Page with Owner details or 404</returns>
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Owner = await _context.Owners
                .Include(o => o.Pets)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (Owner == null) return NotFound();

            return Page();
        }
    }
}
