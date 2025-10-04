using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entites.Identity
{
    public class Contact : BaseEntity<Guid>
    {

        [Required, MaxLength(50)]
        public string FirstName { get; set; } 

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, MaxLength(30)]
        public string PhoneNumber { get; set; } 

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]

        public DateTime Birthdate { get; set; }

        // Link to owner
        [Required]
        public string OwnerId { get; set; }
        public ApplicationUser? Owner { get; set; }
    }
}
