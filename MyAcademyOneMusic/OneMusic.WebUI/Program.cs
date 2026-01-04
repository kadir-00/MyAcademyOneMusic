using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.BusinessLayer.Concrete;
using OneMusic.BusinessLayer.ValidationRules;
using OneMusic.DataAccessLayer.Abstract;
using OneMusic.DataAccessLayer.Concrete;
using OneMusic.DataAccessLayer.Context;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.DAL;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<AppUser, AppRole>(opts =>
{
    opts.User.RequireUniqueEmail = true;
    opts.User.AllowedUserNameCharacters = "abcçdefghiıjklmnoöpqrsştuüvwxyzABCÇDEFGHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789._";
    opts.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<OneMusicContext>().AddErrorDescriber<CustomErrorDescriber>().AddDefaultTokenProviders();

builder.Services.Configure<DataProtectionTokenProviderOptions>(opts => { opts.TokenLifespan = TimeSpan.FromSeconds(60); });

builder.Services.AddScoped<IAboutDal, EfAboutDal>();
builder.Services.AddScoped<IAboutService, AboutManager>();

builder.Services.AddScoped<IAlbumDal, EFAlbumDal>();
builder.Services.AddScoped<IAlbumService, AlbumManager>();

builder.Services.AddScoped<IBannerDal, EfBannerDal>();
builder.Services.AddScoped<IBannerService, BannerManager>();


builder.Services.AddScoped<ISongDal, EFSongDal>();
builder.Services.AddScoped<ISongService, SongManager>();

builder.Services.AddScoped<IMessageService, MessageManager>();
builder.Services.AddScoped<IMessageDal, EFMessageDal>();

builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICategoryDal, EFCategoryDal>();


builder.Services.AddScoped<ISongsListenDetailsService, SongsListenDetailsManager>();
builder.Services.AddScoped<ISongsListenDetailsDal, EFSongsListenDetailsDal>();



builder.Services.AddScoped<IEventService, EventManager>();
builder.Services.AddScoped<IEventDal, EFEventDal>();

builder.Services.AddScoped<IMailService, MailManager>();



builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<OneMusicContext>();


//builder.Services.AddControllersWithViews(opts => { opts.Filters.Add(new AuthorizeFilter()); }).AddJsonOptions(opts => { opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });
builder.Services.AddControllersWithViews().AddJsonOptions(opts => { opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });

builder.Services.ConfigureApplicationCookie(options => { options.LoginPath = "/Login/Index"; options.AccessDeniedPath = "/ErrorPages/Page403"; options.Cookie.Name = Guid.NewGuid().ToString(); options.ExpireTimeSpan = TimeSpan.FromMinutes(1); options.SlidingExpiration = true; options.LogoutPath = "/Login/LogOut/"; });
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseStatusCodePagesWithReExecute("/ErrorPages/Page404", "?code{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

app.Run();
