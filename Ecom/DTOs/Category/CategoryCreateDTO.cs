using System.ComponentModel.DataAnnotations;

namespace Ecommerce.DTOs.Category
{
    public class CategoryCreateDTO:BaseDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime? Datetime { get; set; }
        public string? Username { get; set; }
    }
}
