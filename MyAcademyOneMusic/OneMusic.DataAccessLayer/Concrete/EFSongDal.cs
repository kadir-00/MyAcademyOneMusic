using Microsoft.EntityFrameworkCore;
using OneMusic.DataAccessLayer.Abstract;
using OneMusic.DataAccessLayer.Context;
using OneMusic.DataAccessLayer.Repositories;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.DataAccessLayer.Concrete
{
    public class EFSongDal : GenericRepository<Song>, ISongDal
    {
        private readonly OneMusicContext _context;
        public EFSongDal(OneMusicContext context) : base(context)
        {
            _context = context;
        }

        public List<Song> ArtistSongsWithAlbum(int id)
        {
            return _context.Songs.Include(x => x.Album).Where(x => x.Album.AppUserId == id && x.Album.IsVerify == true).ToList();
        }

        public List<Song> getRandomSingerWithRelationShip()
        {

            var result = _context.Songs.OrderBy(x => Guid.NewGuid()).Include(x => x.Album).Take(4).ToList();
            return result;
        }

        public List<Song> getRandomHitAlbumWithRelationShip()
        {

            var result = _context.Songs.OrderBy(x => Guid.NewGuid()).Include(x => x.Album).ThenInclude(x => x.AppUser).Take(4).ToList();
            return result;
        }

        public List<Song> getSongsByAlbumID(int id)
        {
            return _context.Songs.Include(x => x.Album).ThenInclude(y => y.AppUser).Where(x => x.AlbumId == id && x.Album.IsVerify == true).ToList();
        }

        public int SongCount(int id)
        {
            return _context.Songs.Where(x => x.Album.AppUserId == id && x.Album.IsVerify == true && x.Album.VerifyDescription == "Onaylandı").Count();
        }

        public Song GetRandomBestSong()
        {
            return _context.Songs.Include(x => x.Album).ThenInclude(x => x.AppUser).OrderByDescending(x => Guid.NewGuid()).Take(1).FirstOrDefault();
        }
    }
}
