using FluentValidation;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using OneMusic.BussinesLayer.Abstarct;
using OneMusic.BussinesLayer.Concreate;
using OneMusic.BussinesLayer.Validater;
using OneMusic.DataAccesLayer.Abstarct;
using OneMusic.DataAccesLayer.Concreate;
using OneMusic.DataAccesLayer.Context;
using OneMusic.EntityLayer.Entities;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddIdentity<AppUser,AppRole>().AddEntityFrameworkStores<OneMusicContext>().AddErrorDescriber<CustomErrorDescriber>();

// IAboutDal icerisindeki metotlar EFAboutDal icerisinde yazilmistir dedik asagida 
builder.Services.AddScoped<IAboutDal,EFAboutDal>();
builder.Services.AddScoped<IAboutService,AboutManager>();

builder.Services.AddScoped<IAlbumDal, EFAlbumDal>();
builder.Services.AddScoped<IAlbumService, AlbumManager>();

builder.Services.AddScoped<IBannerDal, EFBannerDal>();
builder.Services.AddScoped<IBannerService, BannerManager>();

builder.Services.AddScoped<ISingerDal, EFSingerDal>();
builder.Services.AddScoped<ISingerService, SingerManager>();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<OneMusicContext>();

builder.Services.AddControllersWithViews(option =>
{ // sadece Yetkili Kisiler Admin Tarafina Erisebilecek Oni Yapacaz
    option.Filters.Add(new AuthorizeFilter());
});

builder.Services.ConfigureApplicationCookie(Options =>
{
    Options.LoginPath = "/Login/Index";

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
