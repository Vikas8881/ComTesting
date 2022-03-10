using System.ComponentModel.DataAnnotations;

namespace Ecommerce.DTOs.Authentication
{
    public class RegistrationDTO
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]

        public string? Password { get; set; }
        [Required]
        public string? Role { get; set; }
    }
}
