using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetManagerRazor.Data;
using PetManagerRazor.Models;

namespace PetManagerRazor.Pages.Pets
{
    /// <summary>
    /// PageModel for editing and deleting a Pet.
    /// </summary>
    public class EditModel : PageModel
    {
        private readonly MyDbContext _context;

        public EditModel(MyDbContext context) => _context = context;

        [BindProperty]
        public Pet Pet { get; set; } = new();

        public List<Owner> Owners { get; set; }

        public List<SelectListItem> SpeciesList { get; set; } = new();

        /// <summary>
        /// Loads the Pet to edit, Owner list, and SpeciesList.
        /// </summary>
        /// <param name="id">Pet Id</param>
        /// <returns>The Page or 404 if Pet not found.</returns>
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Pet = await _context.Pets.FindAsync(id);
            if (Pet == null) return NotFound();

            Owners = await _context.Owners.ToListAsync();

            SpeciesList = PetSpecies.All
                .Select(s => new SelectListItem { Value = s, Text = s })
                .ToList();

            return Page();
        }

        /// <summary>
        /// Handles POST request to update Pet details.
        /// Validates model state and saves changes.
        /// </summary>
        /// <returns>Redirects to Pet list page on success; Redisplays form if invalid; 404 if not found.</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Owners = await _context.Owners.ToListAsync();

                SpeciesList = PetSpecies.All
                    .Select(s => new SelectListItem { Value = s, Text = s })
                    .ToList();

                return Page();
            }

            var existingPet = await _context.Pets.FindAsync(Pet.Id);
            if (existingPet == null) return NotFound();

            existingPet.Name = Pet.Name;
            existingPet.Species = Pet.Species;
            existingPet.Breed = Pet.Breed;
            existingPet.Age = Pet.Age;
            existingPet.AgeUnit = Pet.AgeUnit;
            existingPet.OwnerId = Pet.OwnerId;

            await _context.SaveChangesAsync();

            return RedirectToPage("/Pets/Index");
        }

        /// <summary>
        /// Handles POST request to delete Pet. 
        /// Deletes related Owner if no other Pets are assigned to Owner.
        /// </summary>
        /// <returns>Redirects to Pet list page.</returns>
        public async Task<IActionResult> OnPostDeleteAsync()
        {
            if (Pet.Id == 0) return NotFound();

            var petToDelete = await _context.Pets.FindAsync(Pet.Id);
            if (petToDelete == null) return NotFound();

            var ownerId = petToDelete.OwnerId;

            _context.Pets.Remove(petToDelete);
            await _context.SaveChangesAsync();

            var hasOtherPets = await _context.Pets.AnyAsync(p => p.OwnerId == ownerId);
            if (!hasOtherPets)
            {
                var ownerToDelete = await _context.Owners.FindAsync(ownerId);
                if (ownerToDelete != null)
                {
                    _context.Owners.Remove(ownerToDelete);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage("/Pets/Index");
        }
    }
}
