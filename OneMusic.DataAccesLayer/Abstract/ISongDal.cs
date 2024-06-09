using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.DataAccesLayer.Abstarct
{
    public interface ISongDal : IGenericDal<Song>
    {
        List<Song> GetSongsWithAlbumAndArtist(); // Butun sarkilari getierir

        List<Song> GetSongsWithAlbumByUserId(int id); // Bunuda giris yapan kisiye gore gelecek album ve sarkilar


    }
}
