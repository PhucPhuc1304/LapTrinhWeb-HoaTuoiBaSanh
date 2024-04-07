using CF_HOATUOIBASANH.Interface;
using CF_HOATUOIBASANH.Repositorys;
using CF_HOATUOIBASANH.Models;
using Microsoft.EntityFrameworkCore;
using CF_HOATUOIBASANH.Interfaces;
using CF_HOATUOIBASANH.Authencation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);
// Add support for cookies authentication
builder.Services.AddDbContext<HoaTuoiBaSanhContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductRepository, EFProductRepository>();
builder.Services.AddScoped<ICategoryRepository, EFCategoryRepository>();
builder.Services.AddScoped<IAccountRepository, EFAccountRepository>();
builder.Services.AddScoped<ICustomerRepository, EFCustomerRepository>();
builder.Services.AddScoped<IRoleRepository, EFRoleRepository>();
builder.Services.AddScoped<CustomAuthorizeAttribute>();


builder.Services.AddAuthorization();

builder.Services.AddSession();
builder.Services.AddHttpClient();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication(); 
app.UseAuthorization(); 
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
