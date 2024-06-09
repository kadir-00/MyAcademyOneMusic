using OneMusic.BussinesLayer.Abstarct;
using OneMusic.DataAccesLayer.Abstarct;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BussinesLayer.Concreate
{
    public class SongManager : ISongService
    {
        private readonly ISongDal _songDal;

        public SongManager(ISongDal songDal)
        {
            _songDal = songDal;
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

        public List<Song> TGetSongsWithAlbumAndArtist()
        {
            return _songDal.GetSongsWithAlbumAndArtist();
        }

        public List<Song> TGetSongsWithAlbumByUserId(int id)
        {
           return _songDal.GetSongsWithAlbumByUserId(id);
        }

        public void TUpdate(Song entity)
        {
            _songDal.Update(entity);
        }
    }
}
