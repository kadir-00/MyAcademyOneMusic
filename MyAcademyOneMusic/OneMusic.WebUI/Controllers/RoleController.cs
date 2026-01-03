using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Controllers
{
    // [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var values = _roleManager.Roles.ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        public async Task<IActionResult> DeleteRole(int id)
        {
            var value = await _roleManager.FindByIdAsync(id.ToString());
            await _roleManager.DeleteAsync(value);
            TempData["Result"] = "Rol Silindi";
            TempData["icon"] = "success";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(AppRole appRole)
        {
            await _roleManager.CreateAsync(appRole);
            TempData["Result"] = "Rol Eklendi";
            TempData["icon"] = "success";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateRole(int id)
        {
            var value = await _roleManager.FindByIdAsync(id.ToString());
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRole(AppRole appRole)
        {
            await _roleManager.UpdateAsync(appRole);
            TempData["Result"] = "Rol Güncellendi";
            TempData["icon"] = "success";
            return RedirectToAction("Index");
        }

    }
}
