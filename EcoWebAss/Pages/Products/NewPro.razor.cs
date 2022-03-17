using BootstrapBlazor.Components;
using EcoWebAss.Services.Base;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics.CodeAnalysis;

namespace EcoWebAss.Pages.Products
{
    public partial class NewPro
    {
        [NotNull]
        private Toast? Toast { get; set; }
        private ProductCreateDTO productCreateDTO = new ProductCreateDTO();
        private List<CategoryReadDTO> categorylist { get; set; } = new List<CategoryReadDTO>();

        protected override async Task OnInitializedAsync()
        {
            var response = await categoryService.GetCategory();
            if (response.Success)
            {
                categorylist = response.Data;
            }
        }
        private async Task HandelCreate(EditContext context)
        {
            var response = await proudctService.CreateProduct(productCreateDTO);


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
        private CancellationTokenSource? ReadAvatarToken { get; set; }
        private static long MaxFileLength => 200 * 1024 * 1024;
        private async Task OnAvatarUpload(UploadFile e)
        {
            var file = e.File;
            if (file != null && e.File != null)
            {
                var ext = System.IO.Path.GetExtension(file.Name);
                var format = e.File.ContentType;
                if (CheckValidAvatarFormat(format))
                {
                    ReadAvatarToken ??= new CancellationTokenSource();
                    if (ReadAvatarToken.IsCancellationRequested)
                    {
                        ReadAvatarToken.Dispose();
                        ReadAvatarToken = new CancellationTokenSource();
                    }

                    await e.RequestBase64ImageFileAsync(format, 640, 480, MaxFileLength, ReadAvatarToken.Token);
                    var byteArray = new byte[file.Size];
                    await file.OpenReadStream().ReadAsync(byteArray);
                    string imageType = file.ContentType;
                    string base64String = Convert.ToBase64String(byteArray);

                    productCreateDTO.ImageData = base64String;
                    productCreateDTO.OriginalImageName = file.Name;
                }
                else
                {
                    e.Code = 1;
                    e.Error = "Format Error";
                }
                if (e.Code != 0)
                {
                    await ToastService.Error("Warning ", $"{e.Error} {format}");
                }
            }
        }
        private static bool CheckValidAvatarFormat(string format)
        {
            return "jpg;png;bmp;gif;jpeg".Split(';').Any(f => format.Contains(f, StringComparison.OrdinalIgnoreCase));
        }
    }
}
