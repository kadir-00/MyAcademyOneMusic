using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Controllers
{
    public class SeedDataController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IAlbumService _albumService;
        private readonly IAboutService _aboutService;
        private readonly IBannerService _bannerService;
        private readonly OneMusic.DataAccessLayer.Abstract.IEventDal _eventDal;

        public SeedDataController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IAlbumService albumService,
            IAboutService aboutService, IBannerService bannerService, OneMusic.DataAccessLayer.Abstract.IEventDal eventDal)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _albumService = albumService;
            _aboutService = aboutService;
            _bannerService = bannerService;
            _eventDal = eventDal;
        }

        public async Task<IActionResult> SeedAdminData()
        {
            // 1. Seed About
            if (!_aboutService.TGetList().Any())
            {
                _aboutService.TCreate(new About
                {
                    Title = "OneMusic Hakkında",
                    Description = "OneMusic, müzik tutkunlarını ve yetenekli sanatçıları bir araya getiren yenilikçi bir platformdur. Amacımız, bağımsız sanatçıların seslerini duyurmasına olanak tanırken, dinleyicilere de keşfedilmemiş cevherler sunmaktır.",
                    ImageUrl = "/one-music-gh-pages/img/bg-img/bg-1.jpg"
                });
            }

            // 2. Seed Banner
            if (!_bannerService.TGetList().Any())
            {
                _bannerService.TCreate(new OneMusic.EntityLayer.Entities.Banner
                {
                    Title = "Müziğin Ritmini Hisset",
                    SubTitle = "Keşfetmeye Hazır Mısın?",
                    ImageUrl = "/one-music-gh-pages/img/bg-img/bg-2.jpg"
                });
                _bannerService.TCreate(new OneMusic.EntityLayer.Entities.Banner
                {
                    Title = "Yeni Albümler Yayında",
                    SubTitle = "Şimdi Dinle",
                    ImageUrl = "/one-music-gh-pages/img/bg-img/bg-3.jpg"
                });
            }

            // 3. Seed Events
            if (!_eventDal.GetList().Any())
            {
                _eventDal.Create(new Event
                {
                    Title = "Büyük Yaz Festivali",
                    Location = "İstanbul, Küçükçiftlik Park",
                    Date = DateTime.Now.AddDays(15),
                    ImageURL = "/one-music-gh-pages/img/bg-img/e1.jpg"
                });
                _eventDal.Create(new Event
                {
                    Title = "Akustik Geceler",
                    Location = "İzmir, Ooze Venue",
                    Date = DateTime.Now.AddDays(20),
                    ImageURL = "/one-music-gh-pages/img/bg-img/e2.jpg"
                });
            }

            // 4. Create Pending Albums (Status=False) for AlbumVerify
            // First, make sure we have a user to assign them to.
            var artistUser = await _userManager.FindByNameAsync("hayko");
            if (artistUser != null)
            {

                _albumService.TCreate(new Album
                {
                    AppUserId = artistUser.Id,
                    AlbumName = "Bekleyen Albüm 1 (Demo)",
                    Price = 100,
                    CoverImage = "/one-music-gh-pages/img/bg-img/a10.jpg",
                    IsVerify = false, // PENDING STATUS
                    VerifyDescription = "Onay Aşamasında"
                });

                _albumService.TCreate(new Album
                {
                    AppUserId = artistUser.Id,
                    AlbumName = "Bekleyen Albüm 2 (Live)",
                    Price = 120,
                    CoverImage = "/one-music-gh-pages/img/bg-img/a11.jpg",
                    IsVerify = false, // PENDING STATUS
                    VerifyDescription = "Onay Aşamasında"
                });

            }
            else
            {
                return Content("Error: Artist 'hayko' not found. Please run SeedArtists first.");
            }

            return Content("Admin Data (About, Banner, Events, Pending Albums) Seeded Successfully!");
        }

        public async Task<IActionResult> SeedArtists()
        {
            // Ensure Artist Role
            if (!await _roleManager.RoleExistsAsync("Artist"))
            {
                await _roleManager.CreateAsync(new AppRole { Name = "Artist" });
            }

            // 1. Hayko Cepkin
            await CreateArtist("hayko", "Hayko", "Cepkin", "hayko@onemusic.com", "/one-music-gh-pages/img/bg-img/a1.jpg", new List<Album>
            {
                new Album { AlbumName = "Sandık", Price = 150, CoverImage = "/one-music-gh-pages/img/bg-img/a1.jpg", IsVerify = true, VerifyDescription = "Onaylandı" },
                new Album { AlbumName = "Aşkın Izdırabını...", Price = 180, CoverImage = "/one-music-gh-pages/img/bg-img/a2.jpg", IsVerify = true, VerifyDescription = "Onaylandı" },
                new Album { AlbumName = "Beni Büyüten Şarkılar Vol.1", Price = 200, CoverImage = "/one-music-gh-pages/img/bg-img/a3.jpg", IsVerify = true, VerifyDescription = "Onaylandı" }
            });

            // 2. Sezen Aksu
            await CreateArtist("sezen", "Sezen", "Aksu", "sezen@onemusic.com", "/one-music-gh-pages/img/bg-img/a2.jpg", new List<Album>
            {
                new Album { AlbumName = "Biraz Pop Biraz Sezen", Price = 250, CoverImage = "/one-music-gh-pages/img/bg-img/a4.jpg", IsVerify = true, VerifyDescription = "Onaylandı" },
                new Album { AlbumName = "Demo", Price = 220, CoverImage = "/one-music-gh-pages/img/bg-img/a5.jpg", IsVerify = true, VerifyDescription = "Onaylandı" },
                new Album { AlbumName = "Öptüm", Price = 210, CoverImage = "/one-music-gh-pages/img/bg-img/a6.jpg", IsVerify = true, VerifyDescription = "Onaylandı" }
            });

            // 3. No.1
            await CreateArtist("no1", "Can", "Bozok", "no1@onemusic.com", "/one-music-gh-pages/img/bg-img/a3.jpg", new List<Album>
            {
                new Album { AlbumName = "Siyah Bayrak", Price = 190, CoverImage = "/one-music-gh-pages/img/bg-img/a7.jpg", IsVerify = true, VerifyDescription = "Onaylandı" },
                new Album { AlbumName = "Kron1k", Price = 180, CoverImage = "/one-music-gh-pages/img/bg-img/a8.jpg", IsVerify = true, VerifyDescription = "Onaylandı" },
                new Album { AlbumName = "Çiçeklerde Bir Telaş Var", Price = 170, CoverImage = "/one-music-gh-pages/img/bg-img/a9.jpg", IsVerify = true, VerifyDescription = "Onaylandı" }
            });

            return Content("Artists and Albums Seeded Successfully!");
        }

        private async Task CreateArtist(string username, string name, string surname, string email, string imageUrl, List<Album> albums)
        {
            if (await _userManager.FindByNameAsync(username) == null)
            {
                var user = new AppUser
                {
                    UserName = username,
                    Name = name,
                    Surname = surname,
                    Email = email,
                    ImageURL = imageUrl,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, "Password123!"); // Default password
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Artist");

                    foreach (var album in albums)
                    {
                        album.AppUserId = user.Id;
                        _albumService.TCreate(album);
                    }
                }
            }
        }
    }
}
