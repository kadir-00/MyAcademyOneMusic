using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.Areas.Artist.Models;
using OneMusic.WebUI.ImageSettings;
using System.Web;

namespace OneMusic.WebUI.Areas.Artist.Controllers
{
    [Area("Artist")]
    [Authorize(Roles = "Artist")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMailService _mailService;
        public ProfileController(UserManager<AppUser> userManager, IMailService mailService)
        {
            _userManager = userManager;
            _mailService = mailService;
        }

        async Task<EditArtistViewModel> loadUserModel()
        {
            AppUser user = null;
            if (User.Identity?.Name != null)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }

            if (user == null)
            {
                user = _userManager.Users.FirstOrDefault();
            }

            if (user != null)
            {
                var model = new EditArtistViewModel()
                {
                    ArtistId = user.Id,
                    IsEmailConfirmed = user.EmailConfirmed,
                    Email = user.Email,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    Surname = user.Surname,
                    UserName = user.UserName,
                    ImageURL = user.ImageURL,
                };
                return model;
            }

            // Dummy model if no users exist at all
            return new EditArtistViewModel
            {
                Name = "Misafir",
                Surname = "Sanatçı",
                Email = "misafir@kullanici.com",
                UserName = "misafir",
                ImageURL = "/one-music-gh-pages/img/bg-img/a1.jpg"
            };
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await loadUserModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(EditArtistViewModel model)
        {
            ModelState.Clear();

            AppUser user = null;
            if (User.Identity?.Name != null)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }

            if (user == null)
            {
                user = _userManager.Users.FirstOrDefault();
            }

            if (user == null)
            {
                // No user to update, just return view
                return View(await loadUserModel());
            }

            if (model.ImageFile != null)
            {
                var imageName = ImageSetting.CreateImage(model.ImageFile, "Artists");
                user.ImageURL = imageName;
            }
            string oldUserName = user.UserName;
            user.Surname = model.Surname;
            user.PhoneNumber = model.PhoneNumber;
            user.Name = model.Name;
            user.Email = model.Email;
            user.UserName = model.UserName;

            var result = await _userManager.CheckPasswordAsync(user, model.OldPassword);

            if (result)
            {
                if (model.Password != null)
                {
                    if (model.Password == model.PasswordConfirm)
                    {
                        var PasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);
                        if (!PasswordResult.Succeeded)
                        {
                            foreach (var item in PasswordResult.Errors)
                            {
                                ViewBag.ResultPassword += item.Description + "<br/>";
                            }
                            return View(await loadUserModel());
                        }
                    }
                    else
                    {
                        TempData["Result"] = "Şifreler uyuşmuyor";
                        TempData["icon"] = "info";
                        ModelState.AddModelError("Password", "Şifreler eşleşmiyor.");
                        return View(await loadUserModel());
                    }


                }
                var updateResult = await _userManager.UpdateAsync(user);
                if (updateResult.Succeeded)
                {
                    TempData["Result"] = "Profil bilgileriniz güncellendi";
                    TempData["icon"] = "success";
                    if (oldUserName != user.UserName)
                    {
                        return RedirectToAction("Index", "Login");
                    }


                }
            }
            else
            {
                TempData["Result"] = "Mevcut Şifreniz Hatalı";
                TempData["icon"] = "info";
                ModelState.AddModelError("OldPassword", "Mevcut şifreniz hatalı");
            }
            return View(await loadUserModel());
        }

    }
}
