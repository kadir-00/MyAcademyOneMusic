using Microsoft.EntityFrameworkCore;
using OneMusic.DataAccessLayer.Abstract;
using OneMusic.DataAccessLayer.Context;
using OneMusic.DataAccessLayer.Repositories;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.DataAccessLayer.Concrete
{
    public class EFSongsListenDetailsDal : GenericRepository<SongsListenDetails>, ISongsListenDetailsDal
    {
        private readonly OneMusicContext _context;

        public EFSongsListenDetailsDal(OneMusicContext context) : base(context)
        {
            _context = context;
        }

        public int countByListenedArtist(int id)
        {
            return _context.SongsListenDetails.Include(x => x.Song).ThenInclude(y => y.Album).Where(x => x.Song.Album.AppUserId == id).Count();
        }

        public List<SongsListenDetails> getMostListenSingers()
        {
            var value = _context.SongsListenDetails.Include(x=>x.AppUser).Include(t=>t.Song).OrderByDescending(x => Guid.NewGuid()).ToList();
           
            return value;
        }

        public bool IsActive(int id, int songId)
        {
            var value = _context.SongsListenDetails.FirstOrDefault(x => x.AppUserId == id && x.SongId == songId);
            if (value == null)
            {
                return true;
            }
            return false;
        }
    }
}
