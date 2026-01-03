using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OneMusic.WebUI.Models.BannerModels
{
    public class CreateBannerViewModel
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ImageUrl { get; set; }

        public IFormFile coverPhoto { get; set; } 
    }
}
