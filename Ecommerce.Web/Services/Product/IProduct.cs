using EcommerceWeb.Services.Base;

namespace EcommerceWeb.Services.Product
{
    public interface IProduct
    {
        Task<Response<List<ProductReadDTO>>> GetProduct();
        Task<Response<ProductDetailsDTO>> GetProduct(int ID);
        Task<Response<ProductUpdateDTO>> GetProductForUpdate(int ID);
        Task<Response<int>> CreateProduct(ProductCreateDTO ProductCreateDTO);
        Task<Response<int>> EditProduct(int ID, ProductUpdateDTO ProductUpdateDTO);
        Task<Response<int>> DeleteProduct(int ID);
    }
}
