using EcommerceWeb.Services.Base;

namespace EcommerceWeb.Services.Images
{
    public interface IProductimage
    {
        Task<Response<List<ImageReadDTO>>> GetImage();
        Task<Response<ImageDetailsDTO>> GetImage(int ID);
        Task<Response<ImageUpdateDTO>> GetImageForUpdate(int ID);
        Task<Response<int>> CreateImage(ImageCreateDTO ImageCreateDTO);
        Task<Response<int>> EditImage(int ID, ImageUpdateDTO ImageUpdateDTO);
        Task<Response<int>> DeleteImage(int ID);
    }
}
