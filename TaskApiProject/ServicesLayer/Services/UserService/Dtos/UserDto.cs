using System.ComponentModel.DataAnnotations;

namespace ServicesLayer.Services.UserService.Dtos
{
    public class UserDto 
    {
        public string Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } 
        [Required]
        [MaxLength(50)]
        public DateTime BirthDate { get; set; }
        public string Token { get; set; }

    }
}
