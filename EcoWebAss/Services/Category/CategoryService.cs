using AutoMapper;
using Blazored.LocalStorage;
using EcoWebAss.Services.Base;

namespace EcoWebAss.Services.CategoryService
{
    public class CategoryService : BaseHttpService, ICategory
    {
        public IClient client { get; }
        public IMapper mapper { get; }
        public CategoryService(IClient client, IMapper mapper, ILocalStorageService localStorage) : base(client, localStorage)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public async Task<Response<int>> CreateCategory(CategoryCreateDTO categoryCreateDTO)
        {
           Response<int> response = new Response<int> { Success = true };
            try
            {
                await GetBearerToken();
                await client.CategoriesPOSTAsync(categoryCreateDTO);
            }
            catch (ApiException e)
            {
                response = ConvertApiException<int>(e);
            }
            return response;
        }

        public async Task<Response<int>> DeleteCategory(int ID)
        {
            Response<int> response = new();
            try
            {
                await GetBearerToken();
                await client.CategoriesDELETEAsync(ID);
            }
            catch (ApiException e)
            {
            response=ConvertApiException<int>(e);
            }
            return response;
        }

        public async Task<Response<int>> EditCategory(int ID, CategoryUpdateDTO categoryUpdateDTO)
        {
          Response<int>response= new();
            try
            {
                await GetBearerToken();
                await client.CategoriesPUTAsync(ID, categoryUpdateDTO);
            }
            catch (ApiException e)
            {
              response=ConvertApiException<int>(e);
            }
            return response;
        }

        public async Task<Response<List<CategoryReadDTO>>> GetCategory()
        {
            Response<List<CategoryReadDTO>> response;
            try
            {
                await GetBearerToken();
                var data=await client.CategoriesAllAsync();
                response = new Response<List<CategoryReadDTO>>
                {
                    Data = data.ToList(),
                    Success = true
                };

            }
            catch (ApiException e)
            {
                response =ConvertApiException<List<CategoryReadDTO>>(e);
            }
            return response;
        }

        public async Task<Response<CategoryDetailsDTO>> GetCategories(int ID)
        {
          Response<CategoryDetailsDTO> response;
            try
            {
                await GetBearerToken();
                var data = await client.CategoriesGETAsync(ID);
                response = new Response<CategoryDetailsDTO>
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException e)
            {
                response = ConvertApiException<CategoryDetailsDTO>(e);
            }
            return response;
        }

        public async Task<Response<CategoryUpdateDTO>> GetCategoriyForUpdate(int ID)
        {
          Response<CategoryUpdateDTO> response;
            try
            {
                await GetBearerToken();
                var Data=await client.CategoriesGETAsync(ID);
                response = new Response<CategoryUpdateDTO>
                {
                    Data = mapper.Map<CategoryUpdateDTO>(Data),
                    Success = true
                };
            }
            catch (ApiException e)
            {
            response=ConvertApiException<CategoryUpdateDTO>(e);
            }
            return response;
        }
    }
}
