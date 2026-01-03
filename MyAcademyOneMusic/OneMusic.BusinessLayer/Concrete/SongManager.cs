using OneMusic.BusinessLayer.Abstract;
using OneMusic.DataAccessLayer.Abstract;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BusinessLayer.Concrete
{
    public class SongManager : ISongService
    {
        private readonly ISongDal _songDal;

        public SongManager(ISongDal songDal)
        {
            _songDal = songDal;
        }

        public List<Song> TArtistSongsWithAlbum(int id)
        {
            return _songDal.ArtistSongsWithAlbum(id);
        }

        public void TCreate(Song entity)
        {
            _songDal.Create(entity);
        }

        public void TDelete(int id)
        {
            _songDal.Delete(id);
        }

        public Song TGetById(int id)
        {
            return _songDal.GetById(id);
        }

        public List<Song> TGetList()
        {
            return _songDal.GetList();
        }

        public Song TGetRandomBestSong()
        {
            return _songDal.GetRandomBestSong();
        }

        public List<Song> TgetRandomHitAlbumWithRelationShip()
        {
            return _songDal.getRandomHitAlbumWithRelationShip();
        }

        public List<Song> TgetRandomSingerWithRelationShip()
        {
            return _songDal.getRandomSingerWithRelationShip();
        }

        public List<Song> TgetSongsByAlbumID(int id)
        {
            return _songDal.getSongsByAlbumID(id);
        }

        public int TSongCount(int id)
        {
            return _songDal.SongCount(id);
        }

        public void TUpdate(Song entity)
        {
            _songDal.Update(entity);
        }
    }
}
