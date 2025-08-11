using System.ComponentModel.DataAnnotations;

namespace PetManagerRazor.Models
{
    public class Owner
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, Phone]
        public string PhoneNumber { get; set; } = string.Empty;
        public List<Pet> Pets { get; set; } = new();
    }
}
