using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetManagerRazor.Data;
using PetManagerRazor.Models;

namespace PetManagerRazor.Pages.Owners
{
    /// <summary>
    /// PageModel for editing and deleting Owner.
    /// </summary>
    public class EditModel : PageModel
    {
        private readonly MyDbContext _context;

        public EditModel(MyDbContext context) => _context = context;

        [BindProperty]
        public Owner? Owner { get; set; }

        public int PetCount { get; set; }

        /// <summary>
        /// Loads Owner details for editing, including related Pets.
        /// </summary>
        /// <param name="id">Owner id</param>
        /// <returns>Returns the page with Owner data or 404 if not found.</returns>
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Owner = await _context.Owners
                .Include(o => o.Pets)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (Owner == null) return NotFound();

            PetCount = Owner.Pets.Count;

            return Page();
        }

        /// <summary>
        /// Handles POST request to update Owner details.
        /// Validates model state and saves changes.
        /// </summary>
        /// <returns>
        /// Redirects to Owner details page if successful; Redisplays page for correction if invalid; 404 if Owner not found.
        /// </returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var existingOwner = await _context.Owners.FindAsync(Owner.Id);
            if (existingOwner == null) return NotFound();

            existingOwner.Name = Owner.Name;
            existingOwner.PhoneNumber = Owner.PhoneNumber;
            existingOwner.Email = Owner.Email;

            await _context.SaveChangesAsync();

            return RedirectToPage("/Owners/Details", new { id = Owner.Id });
        }

        /// <summary>
        /// Handles POST request to delete Owner and associated Pets.
        /// </summary>
        /// <returns>Redirects to Owner index page on success; returns 404 if Owner not found.</returns>
        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var owner = await _context.Owners
                .Include(o => o.Pets)
                .FirstOrDefaultAsync(o => o.Id == Owner.Id);

            if (owner == null) return NotFound();

            _context.Pets.RemoveRange(owner.Pets);
            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Owners/Index");
        }

       
    }
}
