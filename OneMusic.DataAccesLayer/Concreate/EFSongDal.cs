using Microsoft.EntityFrameworkCore;
using OneMusic.DataAccesLayer.Abstarct;
using OneMusic.DataAccesLayer.Context;
using OneMusic.DataAccesLayer.Repositories;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.DataAccesLayer.Concreate
{
    public class EFSongDal : GenericRepository<Song>, ISongDal
    {
        private readonly OneMusicContext _context;
        public EFSongDal(OneMusicContext context) : base(context)
        {
            _context = context;
        }

        public List<Song> GetSongsWithAlbumAndArtist()
        {
            return _context.Songs.Include(x=>x.Album).ThenInclude(x=>x.AppUser).ToList();
        }

        public List<Song> GetSongsWithAlbumByUserId(int id)
        {
            return _context.Songs.Include(x=>x.Album).ThenInclude(x=>x.AppUser).Where(x=>x.Album.AppUserId==id).ToList();
        }
    }
}
