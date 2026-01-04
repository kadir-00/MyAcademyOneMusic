using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.Models.RegisterModels;

namespace OneMusic.WebUI.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel model)
        {
            AppUser NewUser = new AppUser()
            {
                Email = model.Email,
                UserName = model.UserName,
                Name = model.Name,
                Surname = model.Surname,
                EmailConfirmed = true,
            };
            var result = await _userManager.CreateAsync(NewUser, model.Password);
            if (model.Password == model.ConfirmPassword)
            {
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(NewUser, "Artist");
                    return RedirectToAction("Index", "Login");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }


            return View();
        }
    }
}
