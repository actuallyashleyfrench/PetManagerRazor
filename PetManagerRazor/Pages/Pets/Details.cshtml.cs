using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetManagerRazor.Data;
using PetManagerRazor.Models;

namespace PetManagerRazor.Pages.Pets
{
    /// <summary>
    /// PageModel for displaying details of a Pet.
    /// </summary>
    public class DetailsModel : PageModel
    {
        private readonly MyDbContext _context;

        public DetailsModel(MyDbContext context) => _context = context;

        public Pet? Pet { get; set; } 

        /// <summary>
        /// Loads Pet by Id including related Owner.
        /// </summary>
        /// <param name="id">Pet's Id</param>
        /// <returns>Page with Pet details or 404 if not found.</returns>
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Pet = await _context.Pets
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (Pet == null) return NotFound();

            return Page();
        }
    }
}
