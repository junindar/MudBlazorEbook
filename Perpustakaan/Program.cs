using System.Reflection;
using DataService.Data;
using DataService.IService;
using DataService.Repository;
using DataService.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

using MudBlazor.Services;
using Perpustakaan.AppSettings;

//using Perpustakaan.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddDbContext<PustakaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("pustakaConnection")));
builder.Services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));

builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IFileUpload, FileUpload>(); builder.Services.AddTransient<IUserService, UserService>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<PustakaDbContext>();
    DbInitializer.Seed(context);


}

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
