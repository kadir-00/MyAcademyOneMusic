using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.ImageSettings;
using OneMusic.WebUI.Models.UserModels;
using System.Web;

namespace OneMusic.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMailService _mailService;

        public AdminProfileController(UserManager<AppUser> userManager, IMailService mailService)
        {
            _userManager = userManager;
            _mailService = mailService;
        }


        async Task<ResultUserViewModel> LoadUserData()
        {
            var userName = User.Identity?.Name;
            if (string.IsNullOrEmpty(userName))
            {
                return new ResultUserViewModel();
            }

            var values = await _userManager.FindByNameAsync(userName);
            if (values == null)
            {
                return new ResultUserViewModel();
            }

            ResultUserViewModel resultUserViewModel = new ResultUserViewModel()
            {
                Name = values.Name ?? "",
                Surname = values.Surname ?? "",
                Email = values.Email ?? "",
                PhoneNumber = values.PhoneNumber ?? "",
                UserName = values.UserName ?? "",
                ImageURL = values.ImageURL ?? "",
                Id = values.Id,
                IsEmailConfirmed = values.EmailConfirmed,
                IsPhoneConfirmed = values.PhoneNumberConfirmed,

            };
            return resultUserViewModel;

        }





        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await LoadUserData();
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Index(ResultUserViewModel resultUserViewModel)
        {
            ModelState.Clear();
            if (resultUserViewModel.OldPassword == null)
            {
                ModelState.AddModelError("OldPassword", "Mevcut şifrenizi giriniz");
                return View(await LoadUserData());
            }

            var values = await _userManager.FindByIdAsync(resultUserViewModel.Id.ToString());
            if (values == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var Pwdresult = await _userManager.CheckPasswordAsync(values, resultUserViewModel.OldPassword);
            if (Pwdresult)
            {
                values.PhoneNumber = resultUserViewModel.PhoneNumber;
                values.UserName = resultUserViewModel.UserName;
                values.Surname = resultUserViewModel.Surname;
                values.Email = resultUserViewModel.Email;
                values.Name = resultUserViewModel.Name;

                if (resultUserViewModel.Password != null)
                {
                    if (resultUserViewModel.Password == resultUserViewModel.ConfirmPassword)
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(values);
                        var passwordResult = await _userManager.ResetPasswordAsync(values, token.ToString(), resultUserViewModel.Password);
                        if (!passwordResult.Succeeded)
                        {
                            foreach (var item in passwordResult.Errors)
                            {
                                ViewBag.ResultPassword += item.Description + "<br/>";
                            }
                            return View(await LoadUserData());
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Girdiğiniz şifreler eşleşmiyor.");
                        return View(await LoadUserData());
                    }
                }

                if (resultUserViewModel.CoverPhoto != null)
                {
                    var result = ImageSetting.CreateImage(resultUserViewModel.CoverPhoto, "Users/Admins");
                    if (values.ImageURL != null)
                    {
                        ImageSetting.DeleteImage(values.ImageURL);
                    }
                    values.ImageURL = result;
                }


                await _userManager.UpdateAsync(values);
                TempData["Result"] = "Profiliniz Güncellendi";
                TempData["icon"] = "success";
                return View(await LoadUserData());
            }
            else
            {
                ModelState.AddModelError("OldPassword", "Mevcut şifreniz hatalı");
                return View(await LoadUserData());
            }

        }

    }
}
