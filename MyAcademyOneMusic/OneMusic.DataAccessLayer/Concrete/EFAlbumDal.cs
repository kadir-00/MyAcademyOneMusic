using Microsoft.EntityFrameworkCore;
using OneMusic.DataAccessLayer.Abstract;
using OneMusic.DataAccessLayer.Context;
using OneMusic.DataAccessLayer.Repositories;
using OneMusic.EntityLayer.Entities;

namespace OneMusic.DataAccessLayer.Concrete
{
    public class EFAlbumDal : GenericRepository<Album>, IAlbumDal
    {
        private readonly OneMusicContext _context;
        public EFAlbumDal(OneMusicContext context) : base(context)
        {
            _context = context;
        }

        public int AlbumCount(int id)
        {
            return _context.Albums.Where(x => x.IsVerify == true && x.AppUserId == id).Count();
        }

        public int AlbumCountByWaiting(int id)
        {
            return _context.Albums.Where(x => x.VerifyDescription == "Onay Aşamasında" && x.AppUserId == id).Count();
        }

        public string ExpensiveAlbumName(int id)
        {
            return _context.Albums.OrderByDescending(x => x.Price).Where(x => x.IsVerify == true && x.AppUserId == id).Select(y => y.AlbumName).FirstOrDefault();
        }

        public List<Album> getAlbumByArtist(int id)
        {
            return _context.Albums.Include(y => y.AppUser).Include(t => t.Songs).Where(x => x.AppUserId == id && x.IsVerify == true).ToList();
        }


        public List<Album> getAlbumListWithArtist()
        {
            return _context.Albums.OrderByDescending(x => Guid.NewGuid()).Include(x => x.AppUser).Where(x => x.IsVerify == true).ToList();
        }

        public List<Album> getListAwatingApprovalAlbums(int id)
        {
            return _context.Albums.Where(x => x.VerifyDescription != "Onaylandı" && x.AppUserId == id).ToList();
        }

        public List<Album> getAlbumWithArtistByStatusFalseList()
        {
            return _context.Albums.Include(x => x.AppUser).Where(x => x.IsVerify == false && x.VerifyDescription == "Onay Aşamasında").ToList();
        }
        public List<Album> getAlbumWithArtistRejectLists()
        {
            return _context.Albums.Include(x => x.AppUser).Where(x => x.IsVerify == false && x.VerifyDescription != "Onay Aşamasında" && x.VerifyDescription != "Onaylandı").ToList();
        }

        public List<Album> getRandomAlbumWithArtist()
        {
            return _context.Albums.OrderByDescending(x => Guid.NewGuid()).Include(y => y.AppUser).Where(x => x.IsVerify == true).Take(4).ToList();
        }


        public Album getAlbumByIDWithAppUser(int id)
        {
            return _context.Albums.Include(x => x.AppUser).FirstOrDefault(x => x.IsVerify == true && x.AlbumId == id);
        }

        public List<Album> getListAlbumWithCategoryAndArtist(string category, string artist)
        {
            return _context.Albums.Include(t => t.AppUser).Include(t => t.Category).Where(x => x.Category.CategoryName == category && x.AppUser.Name + " " + x.AppUser.Surname == artist).ToList();
        }

        public List<Album> getListAlbumWithCategory(string category)
        {
            return _context.Albums.Include(t => t.AppUser).Include(t => t.Category).Where(x => x.Category.CategoryName == category).ToList();
        }

        public List<Album> getListAlbumWithArtist(string artist)
        {
            return _context.Albums.Include(t => t.AppUser).Include(t => t.Category).Where(x => x.AppUser.Name + " " + x.AppUser.Surname == artist).ToList();
        }


    }
}
