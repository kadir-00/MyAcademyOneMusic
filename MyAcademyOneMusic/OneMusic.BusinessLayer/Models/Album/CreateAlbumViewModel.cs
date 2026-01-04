
using Microsoft.AspNetCore.Http;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.BusinessLayer.Models.Album
{
    public class CreateAlbumViewModel
    {
        public string? AlbumName { get; set; }
        public string? CoverImage { get; set; }
        public decimal Price { get; set; }

        public int SingerId { get; set; }
        public int CategoryID { get; set; }


        public IFormFile? Image { get; set; }
    }
}
