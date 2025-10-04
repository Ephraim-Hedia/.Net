using System.ComponentModel.DataAnnotations;

namespace ServicesLayer.Services.ContactService.Dtos
{
    public class ContactCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required, Phone]
        public string PhoneNumber { get; set; }


        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }
    }
}
