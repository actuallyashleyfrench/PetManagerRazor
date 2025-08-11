using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetManagerRazor.Data;
using PetManagerRazor.Models;

namespace PetManagerRazor.Pages.Owners
{
    /// <summary>
    /// PageModel for creating a new Owner.
    /// </summary>
    public class CreateModel : PageModel
    {
        private readonly MyDbContext _context;

        public CreateModel(MyDbContext context) => _context = context;

        [BindProperty]
        public Owner NewOwner { get; set; } = new();

        /// <summary>
        /// Validates form submission to create new Owner in db.
        /// </summary>
        /// <param name="returnTo">The page to redirect to after successful creation.</param>
        /// <returns>Redirects to the given page if successful. Otherwise, reloads the form page with validation errors.</returns>
        public async Task<IActionResult> OnPostAsync(string returnTo)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Owners.Add(NewOwner);
            await _context.SaveChangesAsync();

            return RedirectToPage(returnTo);

        }
        public void OnGet()
        {
        }
    }
}
