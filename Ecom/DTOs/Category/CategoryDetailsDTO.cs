namespace Ecommerce.DTOs.Category
{
    public class CategoryDetailsDTO:CategoryReadDTO
    {
        public List<CategoryReadDTO> CategoryReadDTOs { get; set; }
    }
}
