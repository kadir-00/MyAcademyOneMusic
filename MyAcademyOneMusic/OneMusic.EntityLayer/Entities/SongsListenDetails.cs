using Microsoft.EntityFrameworkCore;


namespace OneMusic.EntityLayer.Entities
{
    public class SongsListenDetails
    {
        public int SongsListenDetailsID { get; set; }
        public int SongId { get; set; } // şarkı id

        public Song Song { get; set; }

        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; } // kim tarafından dinlendi  

        //not müzikleri dinleyebilmek için üye olmak (visitor) gerekli.
    }
}
