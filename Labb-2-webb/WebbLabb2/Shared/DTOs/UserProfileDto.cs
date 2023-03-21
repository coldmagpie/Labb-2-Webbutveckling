using System.ComponentModel.DataAnnotations;

namespace WebbLabb2.Shared.DTOs
{
    public class UserProfileDto
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Adress { get; set; }
    }
}
