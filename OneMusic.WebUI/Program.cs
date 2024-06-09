using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using OneMusic.BussinesLayer.Abstarct;
using OneMusic.BussinesLayer.Abstract;
using OneMusic.BussinesLayer.Concreate;
using OneMusic.BussinesLayer.Validater;
using OneMusic.DataAccesLayer.Abstarct;
using OneMusic.DataAccesLayer.Abstract;
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

builder.Services.AddScoped<IMessageDal, EFMessageDal>();
builder.Services.AddScoped<IMessageService, MessageManager>();

builder.Services.AddScoped<ISongDal, EFSongDal>();
builder.Services.AddScoped<ISongService, SongManager>();

builder.Services.AddScoped<ICategoryDal, EFCategoryDal>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<OneMusicContext>();

builder.Services.AddControllersWithViews(option =>
{ // sadece Yetkili Kisiler Admin Tarafina Erisebilecek Oni Yapacaz

    var authorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();


    option.Filters.Add(new AuthorizeFilter(authorizePolicy));
});

builder.Services.ConfigureApplicationCookie(Options =>
{
    Options.LoginPath = "/Login/Index";
    Options.AccessDeniedPath = "/ErrorPage/AccessDenied";
    Options.LogoutPath = "/LogIn/Logout";

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404","?code{0}");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});


app.Run();
