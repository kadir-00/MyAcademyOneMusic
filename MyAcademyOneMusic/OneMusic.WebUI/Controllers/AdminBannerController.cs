using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.BusinessLayer.Models.Banner;
using OneMusic.BusinessLayer.ValidationRules;
using OneMusic.EntityLayer.Entities;
using OneMusic.WebUI.ImageSettings;

namespace OneMusic.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBannerController : Controller
    {
        private readonly IBannerService _bannerService;

        public AdminBannerController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        public IActionResult Index()
        {
            var lists = _bannerService.TGetList();
            return View(lists);
        }

        [HttpGet]
        public IActionResult CreateBanner()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateBanner(CreateBannerViewModel createBannerViewModel)
        {
            ModelState.Clear();

            if (createBannerViewModel.coverPhoto != null)
            {
                var validator = new BannerValidator();
                var validatorResult = validator.Validate(createBannerViewModel);
                if (validatorResult.IsValid)
                {
                    var result = ImageSetting.CreateImage(createBannerViewModel.coverPhoto, "Banners");
                    Banner newValue = new Banner()
                    {
                        SubTitle = createBannerViewModel.SubTitle,
                        Title = createBannerViewModel.Title,
                        ImageUrl = result,
                    };
                    _bannerService.TCreate(newValue);
                    TempData["Result"] = "Yeni kayıt eklendi";
                    TempData["icon"] = "success";
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in validatorResult.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("coverPhoto", "Görsel Seçiniz");
            }


            TempData["Result"] = "Kayıt Eklenemedi";
            TempData["icon"] = "warning";
            return View();



        }


        [HttpGet]
        public IActionResult UpdateBanner(int id)
        {
            var value = _bannerService.TGetById(id);
            UpdateBannerViewModel updateBannerViewModel = new UpdateBannerViewModel();
            updateBannerViewModel.SubTitle = value.SubTitle;
            updateBannerViewModel.BannerId = value.BannerId;
            updateBannerViewModel.Title = value.Title;

            return View(updateBannerViewModel);
        }
        [HttpPost]
        public IActionResult UpdateBanner(UpdateBannerViewModel updateBannerViewModel)
        {
            ModelState.Clear();

            var value = _bannerService.TGetById(updateBannerViewModel.BannerId);

            value.SubTitle = updateBannerViewModel.SubTitle;
            value.Title = updateBannerViewModel.Title;

            if (updateBannerViewModel.coverPhoto != null)
            {
                var result = ImageSetting.CreateImage(updateBannerViewModel.coverPhoto, "Banners");
                ImageSetting.DeleteImage(value.ImageUrl);
                value.ImageUrl = result;
            }
            _bannerService.TUpdate(value);

            TempData["Result"] = "Kayıt Güncellendi";
            TempData["icon"] = "success";
            return RedirectToAction("Index");
        }
        public IActionResult DeleteBanner(int id)
        {
            var value = _bannerService.TGetById(id);
            ImageSetting.DeleteImage(value.ImageUrl);
            _bannerService.TDelete(id);


            TempData["Result"] = "Kayıt Silindi";
            TempData["icon"] = "success";
            return RedirectToAction("Index");
        }
    }
}
