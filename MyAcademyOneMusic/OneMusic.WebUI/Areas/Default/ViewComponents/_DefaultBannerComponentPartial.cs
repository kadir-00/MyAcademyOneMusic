using Microsoft.AspNetCore.Mvc;
using OneMusic.BusinessLayer.Abstract;

namespace OneMusic.WebUI.Areas.Default.ViewComponents
{
    public class _DefaultBannerComponentPartial : ViewComponent
    {
        private readonly IBannerService _bannerService;

        public _DefaultBannerComponentPartial(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        public IViewComponentResult Invoke()
        {
            var list = _bannerService.TGetList();
            return View(list);
        }
    }
}
