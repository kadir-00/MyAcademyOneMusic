using OneMusic.BusinessLayer.Abstract;
using OneMusic.DataAccessLayer.Abstract;
using OneMusic.DataAccessLayer.Concrete;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BusinessLayer.Concrete
{
    public class SongsListenDetailsManager : ISongsListenDetailsService
    {
        private readonly ISongsListenDetailsDal _songsListenDetailsDal;

        public SongsListenDetailsManager(ISongsListenDetailsDal songsListenDetailsDal)
        {
            _songsListenDetailsDal = songsListenDetailsDal;
        }

        public int TcountByListenedArtist(int id)
        {
          return  _songsListenDetailsDal.countByListenedArtist(id);
        }

        public void TCreate(SongsListenDetails entity)
        {
            _songsListenDetailsDal.Create(entity);
        }

        public void TDelete(int id)
        {
            _songsListenDetailsDal.Delete(id);
        }

        public SongsListenDetails TGetById(int id)
        {
          return _songsListenDetailsDal.GetById(id);
        }

        public List<SongsListenDetails> TGetList()
        {
            return _songsListenDetailsDal.GetList();
        }

        public List<SongsListenDetails> TgetMostListenSingers()
        {
            return _songsListenDetailsDal.getMostListenSingers();
        }

        public bool TIsActive(int id, int songId)
        {
           return _songsListenDetailsDal.IsActive(id, songId);
        }

        public void TUpdate(SongsListenDetails entity)
        {
            _songsListenDetailsDal.Update(entity);
        }
    }
}
