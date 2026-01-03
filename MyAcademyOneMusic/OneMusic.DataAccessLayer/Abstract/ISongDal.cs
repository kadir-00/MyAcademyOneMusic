using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.DataAccessLayer.Abstract
{
    public interface ISongDal:IGenericDal<Song>
    {
        List<Song> getRandomSingerWithRelationShip();

        int SongCount(int id);

        List<Song> ArtistSongsWithAlbum(int id);
        List<Song> getSongsByAlbumID(int id);

        List<Song> getRandomHitAlbumWithRelationShip();

        Song GetRandomBestSong();


    }
}
