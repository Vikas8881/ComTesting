using AutoMapper;
using Blazored.LocalStorage;
using EcommerceWeb.Services.Base;

namespace EcommerceWeb.Services.Product
{
    public class ProductService : BaseHttpService, IProduct
    {
        private readonly IClient client;
        private readonly IMapper mapper;

        public ProductService(IClient client, IMapper mapper, ILocalStorageService localStorage) : base(client, localStorage)
        {
            this.client = client;
            this.mapper = mapper;

        }

        public async Task<Response<int>> CreateProduct(ProductCreateDTO ProductCreateDTO)
        {
            Response<int> response = new Response<int> { Success = true };
            try
            {
                await GetBearerToken();
                await client.ProductsPOSTAsync(ProductCreateDTO);
            }
            catch (ApiException e)
            {
                response = ConvertApiException<int>(e);
            }
            return response;
        }

        public async Task<Response<int>> DeleteProduct(int ID)
        {
            Response<int> response = new();
            try
            {
                await GetBearerToken();
                await client.ProductsDELETEAsync(ID);
            }
            catch (ApiException e)
            {
                response = ConvertApiException<int>(e);
            }
            return response;
        }

        public async Task<Response<int>> EditProduct(int ID, ProductUpdateDTO ProductUpdateDTO)
        {
            Response<int> response = new();
            try
            {
                await GetBearerToken();
                await client.ProductsPUTAsync(ID, ProductUpdateDTO);
            }
            catch (ApiException e)
            {
                response = ConvertApiException<int>(e);
            }
            return response;
        }

        public async Task<Response<List<ProductReadDTO>>> GetProduct()
        {
            Response<List<ProductReadDTO>> response;
            try
            {
                await GetBearerToken();
                var data = await client.ProductsAllAsync();
                response = new Response<List<ProductReadDTO>>
                {
                    Data = data.ToList(),
                    Success = true
                };

            }
            catch (ApiException e)
            {
                response = ConvertApiException<List<ProductReadDTO>>(e);
            }
            return response;
        }

        public async Task<Response<ProductDetailsDTO>> GetProduct(int ID)
        {
            Response<ProductDetailsDTO> response;
            try
            {
                await GetBearerToken();
                var data = await client.ProductsGETAsync(ID);
                response = new Response<ProductDetailsDTO>
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException e)
            {
                response = ConvertApiException<ProductDetailsDTO>(e);
            }
            return response;
        }

        public async Task<Response<ProductUpdateDTO>> GetProductForUpdate(int ID)
        {
            Response<ProductUpdateDTO> response;
            try
            {
                await GetBearerToken();
                var data = await client.ProductsGETAsync(ID);
                response = new Response<ProductUpdateDTO>
                {
                    Data = mapper.Map<ProductUpdateDTO>(data),
                    Success = true
                };
            }
            catch (ApiException e)
            {
                response = ConvertApiException<ProductUpdateDTO>(e);
            }
            return response;
        }
    }
}