using System.ComponentModel.DataAnnotations;

namespace Ecommerce.DTOs.Authentication
{
    public class LoginUserDTO
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
