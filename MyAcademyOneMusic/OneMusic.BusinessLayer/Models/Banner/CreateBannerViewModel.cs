using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OneMusic.BusinessLayer.Models.Banner
{
    public class CreateBannerViewModel
    {
        public int BannerId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ImageUrl { get; set; }

        public IFormFile coverPhoto { get; set; } 
    }
}
