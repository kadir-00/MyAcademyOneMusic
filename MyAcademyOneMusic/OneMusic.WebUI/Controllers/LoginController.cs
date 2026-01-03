using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.Models.LoginModels;
using System.Web;

namespace OneMusic.WebUI.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMailService _mailService;


        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IMailService mailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mailService = mailService;
        }
        [HttpGet]
        public IActionResult Index(string returnUrl)
        {
            ModelState.Clear();
            if (returnUrl == "1")
            {
                ModelState.AddModelError("", "Mail adresiniz güncellendi");
            }
   
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model, string? returnUrl)
        {
            await _signInManager.SignOutAsync();
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(User.Identity.Name);
                    var ArtistResult = await _userManager.IsInRoleAsync(user, "Artist");
                    var AdminResult = await _userManager.IsInRoleAsync(user, "Admin");
                    if (ArtistResult)
                    {
                        if (returnUrl != null)
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToAction("Index", "MyAlbum", new { area = "artist" });
                    }
                    else if (AdminResult)
                    {
                        if (returnUrl != null)
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToAction("Index", "AdminAbout");
                    }
                    else
                    {
                        if (returnUrl != null)
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
                }
            }

            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Login");

        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            ModelState.Clear();
            if (string.IsNullOrEmpty(email))
            {

                ModelState.AddModelError("", "Email adresinizi yazınız");
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    if (user.EmailConfirmed)
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                        _mailService.sendMail(email, "Şifre Sıfırlama", $"Merhaba, Şifre Sıfırlama Talebini Aldık, Butona Tıklayarak Şifreni Yenileyebilirsin. <br><br>Bu işlem size ait değilse lütfen bizimle iletişime geçiniz. <br><br> <a target=\"blank\" style=\"appearance: none; text-decoration: none; height:35px; width:200px; background-color: #2ea44f; border: 1px solid rgba(27, 31, 35, .15);  border-radius: 6px;  box-shadow: rgba(27, 31, 35, .1) 0 1px 0;  box-sizing: border-box;  color: #fff; cursor: pointer; text-align:center;  display: inline-block; font-family: -apple-system,system-ui,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji;  font-size: 14px;  font-weight: 600; line-height: 20px;  padding: 6px 16px;  position: relative; text-align: center;  text-decoration: none;  user-select: none;  -webkit-user-select: none; touch-action: manipulation;  vertical-align: middle; white-space: nowrap;\"  target=\"_blank\" href=\"https://localhost:7238{Url.Action("ResetPassword", "Login", new { id = user.Id, token = HttpUtility.UrlEncode(token) })}\">Şifre Güncelle</a><br><br> One Music");

                        ModelState.AddModelError("", "Email adresinize şifre sıfırlama istediğini yolladık lütfen kontrol edin.");
                    }
                    else
                    {
                        TempData["userErr"] = "emailnotconfirmed";
                        TempData["userMail"] = email;
                    }



                }
                else
                {
                    ModelState.AddModelError("", "Email adresinizi kontrol ediniz.");
                }
            }
            return View();
        }
        [HttpGet("[action]/{id}/{token}")]
        public async Task<IActionResult> ResetPassword(string id, string token)
        {
            return View();
        }
        [HttpPost("[action]/{id}/{token}")]
        public async Task<IActionResult> ResetPassword(string id, string token, string pwd, string confrmpwd)
        {

            if (pwd == confrmpwd)
            {

                AppUser user = await _userManager.FindByIdAsync(id);
                var result = await _userManager.ResetPasswordAsync(user, token, pwd);
                if (result.Succeeded)
                {
                    await _signInManager.SignOutAsync();
                    await _userManager.UpdateAsync(user);
                    _mailService.sendMail(user.Email, "Şifre Başarıyla Sıfırlandı", "Merhaba, <h3 style=text-transform:capitalize>" + user.Name + " " + user.Surname + "</h3> birkaç dakika önce şifreniz sıfırlandı bu işlem size ait değilse lütfen iletişime geçiniz. <br> One Music");

                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {

                        var err = string.Join("<br>", item.Description);
                        ViewBag.Err = err;
                    }
                }
            }

            return View();
        }


        public async Task<JsonResult> ConfirmEmail()
        {
            var email = TempData["userMail"].ToString();
            AppUser user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                _mailService.sendMail(email, "Mail Doğrulama", $"Merhaba,<br><br>Mailinizi aşağıdaki url üzerinden doğrulayabilirsiniz. <br/ ><a href=\"https://localhost:7238{Url.Action("EmailConfirmed", "Login", new { id = user.Id, token = HttpUtility.UrlEncode(token) })}\">Mailimi Doğrula</a><br><br> One Music");

            }

            return Json(null);
        }
        [HttpGet("[action]/{id}/{token}")]
        public async Task<IActionResult> EmailConfirmed(string id, string token, string returnUrl)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            var result = await _userManager.ConfirmEmailAsync(user, HttpUtility.UrlDecode(token));

            if (result.Succeeded)
            {
                await _userManager.UpdateAsync(user);
                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return Redirect("/Login/Index?returnUrl=1");
                }
            }
            else
            {
                return BadRequest();
            }


        }


    }
}
