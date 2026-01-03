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
    //[Authorize(Roles = "Admin")]
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
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            ResultUserViewModel resultUserViewModel = new ResultUserViewModel()
            {
                Name = values.Name,
                Surname = values.Surname,
                Email = values.Email,
                PhoneNumber = values.PhoneNumber,
                UserName = values.UserName,
                ImageURL = values.ImageURL,
                Id = values.Id,
                IsEmailConfirmed = values.EmailConfirmed,
                IsPhoneConfirmed = values.PhoneNumberConfirmed,

            };
            return resultUserViewModel;

        }

        public async Task<IActionResult> SendMailForVerifyMail(int id)
        {
            AppUser user = await _userManager.FindByIdAsync(id.ToString());
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            _mailService.sendMail(user.Email, "Mail Doğrulama", $"Merhaba,<br><br>Mailinizi aşağıdaki url üzerinden doğrulayabilirsiniz. <br/ ><a href=\"https://localhost:7238{Url.Action("EmailConfirmed", "Login", new { id = user.Id, token = HttpUtility.UrlEncode(token) })}\">Mailimi Doğrula</a><br><br> One Music");
            TempData["Result"] = "Aktivasyon Kodu Gönderildi aktivasyon süresi 1 dakikadır";
            TempData["icon"] = "success";
            TempData["userEmailConfirmedTimer"] = "success";
            return RedirectToAction("Index");
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
            var values = await _userManager.FindByIdAsync(resultUserViewModel.Id.ToString());
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
                    ImageSetting.DeleteImage(values.ImageURL);
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
