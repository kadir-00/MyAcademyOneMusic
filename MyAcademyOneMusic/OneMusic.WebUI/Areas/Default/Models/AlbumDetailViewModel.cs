using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Areas.Default.Models
{
    public class AlbumDetailViewModel
    {
        public int AlbumId { get; set; }
        public string AlbumName { get; set; }
        public string CoverImage { get; set; }
        public string SongName { get; set; }
        public string SongURL { get; set; }

        public int AppUserId { get; set; }




    }
}
