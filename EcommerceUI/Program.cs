using Blazored.LocalStorage;
using BootstrapBlazor.Components;

using EcommerceWeb.Services.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using EcommerceWeb.Configuration;
using Syncfusion.Blazor;
using EcommerceWeb.Services.CategoryService;
using EcommerceWeb.Services.Product;
using EcommerceWeb.Services.Images;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddBootstrapBlazor();
builder.Services.AddScoped<ToastService>();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddHttpClient<IClient, Client>(cl =>
cl.BaseAddress = new Uri("https://localhost:7241"));
builder.Services.AddScoped<ICategory, CategoryService>();
builder.Services.AddScoped<IProduct, ProductService>();
builder.Services.AddScoped<IProductimage, ProductImageService>();
builder.Services.AddBootstrapBlazor(options =>
{
    
    options.ToastDelay = 15000;
});


//Mapper
builder.Services.AddAutoMapper(typeof(MapperConfig));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
