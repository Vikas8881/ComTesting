using BootstrapBlazor.Components;
using EcommerceWeb.Services.Base;
using EcommerceWeb.Services.CategoryService;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics.CodeAnalysis;

namespace EcommerceWeb.Pages.Category
{
    public sealed partial class Category
    {
        [NotNull]
        private Toast? Toast { get; set; }
        private CategoryCreateDTO categoryCreateDTO=new CategoryCreateDTO();
        protected override async Task OnInitializedAsync()
        {
            
        }
        private async Task HandelCreate(EditContext context)
        {
            var response = await categoryRegistration.CreateCategory(categoryCreateDTO);


            if (response.Success)
            {
                await ToastService.Show(new ToastOption()
                {
                    IsAutoHide = true,
                    Category = ToastCategory.Success,
                    Title = "Saved Successfully",
                    Content = "Data Saved Successfully"
                });
            }
        }
    }
}
