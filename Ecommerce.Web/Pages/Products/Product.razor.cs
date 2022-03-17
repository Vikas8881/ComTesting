using EcommerceWeb.Services.Base;

namespace EcommerceWeb.Pages.Products
{
    public partial class Product
    {
        private List<ProductReadDTO> productlist;

        private Response<List<ProductReadDTO>> response = new Response<List<ProductReadDTO>> { Success = true };

        protected override async Task OnInitializedAsync()
        {
            response = await proudctService.GetProducts();
            if (response.Success)
            {
                productlist = response.Data;
            }
          
        }
    }
}
