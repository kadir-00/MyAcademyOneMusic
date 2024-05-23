using Microsoft.AspNetCore.Mvc;
using OneMusic.BussinesLayer.Abstarct;

namespace OneMusic.WebUI.ViewComponents.Default_Index
{
    public class _DefaultBannerComponent : ViewComponent
    {
        private readonly IBannerService _bannerService; 

        public _DefaultBannerComponent(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        public IViewComponentResult Invoke() 
        {
            var values = _bannerService.TGetList();
            return View(values); 
        
        }

    }
}
