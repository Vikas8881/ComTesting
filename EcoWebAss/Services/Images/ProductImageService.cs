using AutoMapper;
using Blazored.LocalStorage;
using EcoWebAss.Services.Base;
using EcoWebAss.Services.Images;
using EcoWebAss.Services.Product;

namespace EcoWebAss.Services.Product
{
    public class ProductImageService : BaseHttpService, IProductimage
    {
        private readonly IClient client;
        private readonly IMapper mapper;

        public ProductImageService(IClient client, IMapper mapper, ILocalStorageService localStorage) : base(client, localStorage)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public async Task<Response<int>> CreateImage(ImageCreateDTO ImageCreateDTO)
        {
            Response<int> response = new Response<int> { Success = true };
            try
            {
                await GetBearerToken();
                await client.ProductImagePOSTAsync(ImageCreateDTO);
            }
            catch (ApiException e)
            {
                response = ConvertApiException<int>(e);
            }
            return response;
        }

        public async Task<Response<int>> DeleteImage(int ID)
        {
            Response<int> response = new();
            try
            {
                await GetBearerToken();
                await client.ProductImageDELETEAsync(ID);
            }
            catch (ApiException e)
            {
                response = ConvertApiException<int>(e);
            }
            return response;
        }

        public async Task<Response<int>> EditImage(int ID, ImageUpdateDTO ImageUpdateDTO)
        {
            Response<int> response = new();
            try
            {
                await GetBearerToken();
                await client.ProductImagePUTAsync(ID, ImageUpdateDTO);
            }
            catch (ApiException e)
            {
                response = ConvertApiException<int>(e);
            }
            return response;
        }

        public async Task<Response<List<ImageReadDTO>>> GetImage()
        {
            Response<List<ImageReadDTO>> response;
            try
            {
                await GetBearerToken();
                var data = await client.ProductImageAllAsync();
                response = new Response<List<ImageReadDTO>>
                {
                    Data = data.ToList(),
                    Success = true
                };

            }
            catch (ApiException e)
            {
                response = ConvertApiException<List<ImageReadDTO>>(e);
            }
            return response;
        }

        public async Task<Response<ImageDetailsDTO>> GetImage(int ID)
        {
            Response<ImageDetailsDTO> response;
            try
            {
                await GetBearerToken();
                var data = await client.ProductImageGETAsync(ID);
                response = new Response<ImageDetailsDTO>
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException e)
            {
                response = ConvertApiException<ImageDetailsDTO>(e);
            }
            return response;
        }

        public async Task<Response<ImageUpdateDTO>> GetImageForUpdate(int ID)
        {
            Response<ImageUpdateDTO> response;
            try
            {
                await GetBearerToken();
                var data = await client.ProductImageGETAsync(ID);
                response = new Response<ImageUpdateDTO>
                {
                    Data = mapper.Map<ImageUpdateDTO>(data),
                    Success = true
                };
            }
            catch (ApiException e)
            {
                response = ConvertApiException<ImageUpdateDTO>(e);
            }
            return response;
        }
    }
}
