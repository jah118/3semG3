using System.ComponentModel.DataAnnotations;

namespace RestaurantWebApp.DataTransferObject
{
    public class CustomerDTO
    {
        public CustomerDTO()
        {
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Tlf")]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Navn")]
        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Efternavn")]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Adresse")]
        [StringLength(50, MinimumLength = 3)]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Postnummer")]
        [StringLength(50, MinimumLength = 3)]
        public string ZipCode { get; set; }

        [Required]
        [Display(Name = "By")]
        [StringLength(50, MinimumLength = 3)]
        public string City { get; set; }
    }
}