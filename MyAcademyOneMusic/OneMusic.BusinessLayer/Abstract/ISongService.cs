using OneMusic.DataAccessLayer.Repositories;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BusinessLayer.Abstract
{
    public interface ISongService : IGenericService<Song>
    {
        List<Song> TgetRandomSingerWithRelationShip();
        int TSongCount(int id);
        List<Song> TArtistSongsWithAlbum(int id);
        List<Song> TgetSongsByAlbumID(int id);
        List<Song> TgetRandomHitAlbumWithRelationShip();

        Song TGetRandomBestSong();
        List<Song> TGetLast6Songs();
        List<Song> TGetSongsOrderedByDate(int maxPerArtist, int totalCount);
    }

}
