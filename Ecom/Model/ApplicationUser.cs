using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
