using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetManagerRazor.Data;
using PetManagerRazor.Models;
using System.Threading.Tasks;

namespace PetManagerRazor.Pages.Pets
{
    /// <summary>
    /// PageModel for creating a new Pet.
    /// Loads owners and species list, handles form submission, 
    /// and redirects to add a new owner while preserving form state.
    /// </summary>
    public class CreateModel : PageModel
    {
        private readonly MyDbContext _context;

        public CreateModel(MyDbContext context) => _context = context;

        [BindProperty]
        public Pet NewPet { get; set; } = new();

        public List<SelectListItem> SpeciesList { get; set; } = new();

        public List<Owner> Owners { get; set; } = new();

        /// <summary>
        /// Loads Owners and Species List. Restores any TempData values if 
        /// redirected back from adding a new Owner.
        /// </summary>
        public async Task OnGetAsync()
        {
            Owners = await _context.Owners.ToListAsync();

            SpeciesList = PetSpecies.All
                .Select(s => new SelectListItem {  Value = s, Text = s })
                .ToList();

            if (TempData.ContainsKey("PetName"))
                NewPet.Name = TempData["PetName"]?.ToString() ?? "";

            if (TempData.ContainsKey("PetSpecies"))
                NewPet.Species = TempData["PetSpecies"]?.ToString() ?? "";

            if (TempData.ContainsKey("PetBreed"))
                NewPet.Breed = TempData["PetBreed"]?.ToString() ?? "";

            if (TempData.ContainsKey("PetAge") && int.TryParse(TempData["PetAge"]?.ToString(), out int age))
                NewPet.Age = age;

            if (TempData.ContainsKey("PetAgeUnit"))
                NewPet.AgeUnit = TempData["PetAgeUnit"]?.ToString() ?? "";

            if (TempData.ContainsKey("PetOwnerId") && int.TryParse(TempData["PetOwnerId"]?.ToString(), out int ownerId))
                NewPet.OwnerId = ownerId;

        }

        /// <summary>
        /// Handles POST request to create a new Pet.
        /// Validates form input and saves new Pet to DB.
        /// </summary>
        /// <returns>Redirects to Pets index on success; Redisplays form on validation error.</returns>
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

            _context.Pets.Add(NewPet);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Pets/Index");
        }

        /// <summary>
        /// Handles POST request ot redirect to Add Owner page.
        /// Preserves current form data.
        /// </summary>
        /// <returns>Redirect to Add Owner page with returnTo for redirecting back to Add Pet page.</returns>
        public IActionResult OnPostGoToAddOwner()
        {
            TempData["PetName"] = NewPet.Name;
            TempData["PetSpecies"] = NewPet.Species;
            TempData["PetBreed"] = NewPet.Breed;
            TempData["PetAge"] = NewPet.Age;
            TempData["PetAgeUnit"] = NewPet.AgeUnit;
            TempData["PetOwnerId"] = NewPet.OwnerId;

            return RedirectToPage("/Owners/Create", new { returnTo = "/Pets/Create" });

        }
    }
}
