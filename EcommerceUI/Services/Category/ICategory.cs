using EcommerceWeb.Services.Base;

namespace EcommerceWeb.Services.CategoryService
{
    public interface ICategory
    {
        Task<Response<List<CategoryReadDTO>>> GetCategories();
        Task<Response<CategoryDetailsDTO>> GetCategories(int ID);
        Task<Response<CategoryUpdateDTO>> GetCategoriyForUpdate(int ID);
        Task<Response<int>>CreateCategory(CategoryCreateDTO categoryCreateDTO); 
        Task<Response<int>>EditCategory(int ID,CategoryUpdateDTO categoryUpdateDTO);
        Task<Response<int>>DeleteCategory(int ID);
    }
}
