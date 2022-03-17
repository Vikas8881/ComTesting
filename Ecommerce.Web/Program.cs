using Blazored.LocalStorage;
using BootstrapBlazor.Components;
using EcommerceWeb;
using EcommerceWeb.Configuration;
using EcommerceWeb.Services.Base;
using EcommerceWeb.Services.CategoryService;
using EcommerceWeb.Services.Images;
using EcommerceWeb.Services.Product;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7241") });
builder.Services.AddHttpClient<IClient, Client>(cl =>
cl.BaseAddress = new Uri("https://localhost:7241"));
builder.Services.AddBootstrapBlazor();
builder.Services.AddScoped<ToastService>();
builder.Services.AddBlazoredLocalStorage();
//builder.Services.AddHttpClient<IClient, Client>();
builder.Services.AddScoped<ICategory, CategoryService>();
builder.Services.AddScoped<IProduct, ProductService>();
builder.Services.AddScoped<IProductimage, ProductImageService>();
builder.Services.AddBootstrapBlazor(options =>
{

    options.ToastDelay = 15000;
});


//Mapper
builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddSyncfusionBlazor(options => { options.IgnoreScriptIsolation = true; });
await builder.Build().RunAsync();
