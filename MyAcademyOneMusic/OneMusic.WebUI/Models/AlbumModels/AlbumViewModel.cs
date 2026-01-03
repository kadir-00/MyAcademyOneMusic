using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Models.AlbumModels
{
    public class AlbumViewModel
    {
        public int AlbumId { get; set; }
        public string AlbumName { get; set; }
        public string CoverImage { get; set; }
        public decimal Price { get; set; }



        public List<Song> Songs { get; set; }


        public IFormFile coverImage { get; set; }
    }
}
