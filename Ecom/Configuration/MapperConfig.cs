using AutoMapper;
using Ecommerce.DTOs.Authentication;
using Ecommerce.DTOs.Category;
using Ecommerce.DTOs.Images;
using Ecommerce.DTOs.Product;
using Ecommerce.Model;

namespace Ecom.Configuration
{
    public class MapperConfig:Profile
    {
        public MapperConfig()
        {
            //Category 
            CreateMap<CategoryCreateDTO,Category>().ReverseMap();
            CreateMap<CategoryReadDTO,Category>().ReverseMap();
            CreateMap<CategoryDetailsDTO,Category>().ReverseMap();
            CreateMap<CategoryUpdateDTO,Category>().ReverseMap();
            
            //Product 
            CreateMap<ProductCreateDTO, Product>().ReverseMap();
            CreateMap<ProductReadDTO, Product>().ReverseMap();
            CreateMap<ProductDetailsDTO, Product>().ReverseMap();
            CreateMap<ProductUpdateDTO, Product>().ReverseMap();
            //Image
            CreateMap<ImageCreateDTO, Product_Image>().ReverseMap();
            CreateMap<ImageReadDTO, Product_Image>().ReverseMap();
            CreateMap<ImageDetailsDTO, Product_Image>().ReverseMap();
            CreateMap<ImageUpdateDTO, Product_Image>().ReverseMap();

            //Authentication
            CreateMap<ApplicationUser, RegistrationDTO>().ReverseMap();
        }
    }
}
