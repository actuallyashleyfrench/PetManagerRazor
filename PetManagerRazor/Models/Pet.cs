using System.ComponentModel.DataAnnotations;

namespace PetManagerRazor.Models
{
    public class Pet
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Species is required.")]
        public string Species { get; set; } = string.Empty;

        [Display(Name = "Breed")]
        public string? Breed { get; set; } 

        [Range(1, int.MaxValue, ErrorMessage = "Age must be valid")]
        public int Age { get; set; }

        [Required]
        public string AgeUnit { get; set; } = "Years";

        [Required]
        public int OwnerId { get; set; }
        public Owner? Owner { get; set; }
    }
}
