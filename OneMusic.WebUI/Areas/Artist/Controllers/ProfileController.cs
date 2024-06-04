using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.Areas.Artist.Models;

namespace OneMusic.WebUI.Areas.Artist.Controllers
{
    [Area("Artist")] // Buranin area icerisinde oldugunu belirtmemiz lazim belirttik

    [Authorize(Roles = "Artist")] // Sadece Rolu artist olan kisiler bu sayfaya erisebilirler

    [Route("[area]/[controller]/[action]/{id?}")]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;

        }

        public async Task< IActionResult > Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = new ArtistEditViewModel
            {


                Mail = user.Email,
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
                Surname = user.SurName,
                ImageUrl = user.ImageUrl,
                Username = user.UserName
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ArtistEditViewModel model)
        {
            ModelState.Clear();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (model.ImageFile != null)
            {
                var resource = Directory.GetCurrentDirectory(); // Suanki projenin yolunu bul diyoruz 
                var extansion = Path.GetExtension(model.ImageFile.FileName).ToLower(); // sectigimiz dosyanin uzantisini aldirdik
                if (extansion != ".jpeg" && extansion != ".jpg" && extansion != ".png")
                {
                    // desteklenmeyen dosya uzantisi hatasi 
                    ModelState.AddModelError("ImageFile", "Sadece JPEG VE YA JPG DOSYALARI");
                    // GEREKIRSE Islem Sonlandirma 
                    return View(model);
                }
                var imageName = Guid.NewGuid() + extansion;    // dosyanin ismini aliyorruz 
                var saveLocation = resource + "/wwwroot/images/" + imageName;     //kaydedecegimiz yer
                var stream = new FileStream(saveLocation, FileMode.Create); // kaydetme islemi
                await model.ImageFile.CopyToAsync(stream);
                user.ImageUrl = "/Images/" + imageName;
             }

            


            user.Name = model.Name;
            user.SurName = model.Surname;
            user.PhoneNumber = model.PhoneNumber;
            user.Email = model.Mail;
            user.UserName = model.Username;

            var result = await _userManager.CheckPasswordAsync(user, model.OldPassword); //BURADA TEKRAR GIRILEN SIFRENIN DOGRULUGUNU KONTROL EDECEZ 
            if (result == true)
            {
                if (model.NewPassword != null && model.ConfirmPassword == model.NewPassword)
                {
                    var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (!changePasswordResult.Succeeded)
                    {
                        foreach (var item in changePasswordResult.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                            return View();
                        }

                    }
                }

                var updateResult = await _userManager.UpdateAsync(user);
                if (updateResult.Succeeded)
                {
                    return RedirectToAction("Index","Login");
                }
            }
            ModelState.AddModelError("","Mevcut Sifreniz Hatali ");
            return View();
        }

    }
}
