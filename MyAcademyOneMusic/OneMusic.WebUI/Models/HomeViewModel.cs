using OneMusic.EntityLayer.Entities;
using System.Collections.Generic;

namespace OneMusic.WebUI.Models
{
    public class HomeViewModel
    {
        public List<Album> LatestAlbums { get; set; }
        public List<Album> BuyNowAlbums { get; set; }
        public Album FeaturedAlbum { get; set; }
        public List<AppUser> PopularArtists { get; set; }
        public List<Song> NewHitSongs { get; set; }
        public List<Album> WeeksTopAlbums { get; set; }
    }
}
