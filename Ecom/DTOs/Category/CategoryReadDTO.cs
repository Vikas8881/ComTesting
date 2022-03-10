namespace Ecommerce.DTOs.Category
{
    public class CategoryReadDTO:BaseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Datetime { get; set; }
        public string Username { get; set; }
    }
}
