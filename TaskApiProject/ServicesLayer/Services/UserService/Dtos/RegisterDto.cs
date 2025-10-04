using System.ComponentModel.DataAnnotations;

namespace ServicesLayer.Services.UserService.Dtos
{
    public class RegisterDto
    {
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
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/1/1900", "12/31/2100",
            ErrorMessage = "Date of birth must be between 1900 and 2100")]
        public DateTime BithDate { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
