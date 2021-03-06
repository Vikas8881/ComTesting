using Blazored.LocalStorage;
using BootstrapBlazor.Components;
using EcoWebAss;
using EcoWebAss.Configuration;
using EcoWebAss.Services.Base;
using EcoWebAss.Services.CategoryService;
using EcoWebAss.Services.Images;
using EcoWebAss.Services.Product;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7241") });
//builder.Services.AddHttpClient<IClient, Client>(cl =>
//cl.BaseAddress = new Uri("https://localhost:7241"));
//builder.Services.AddBootstrapBlazor();
builder.Services.AddScoped<ToastService>();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddHttpClient<IClient, Client>();
builder.Services.AddScoped<ICategory, CategoryService>();
builder.Services.AddScoped<IProduct, ProductService>();
builder.Services.AddScoped<IProductimage, ProductImageService>();
builder.Services.AddBootstrapBlazor(options =>
{

    options.ToastDelay = 15000;
});


//Mapper
builder.Services.AddAutoMapper(typeof(MapperConfig));


await builder.Build().RunAsync();
