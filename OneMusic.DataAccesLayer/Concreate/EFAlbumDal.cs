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
    public class EFAlbumDal : GenericRepository<Album>, IAlbumDal

    {
        private readonly OneMusicContext _context;
        public EFAlbumDal(OneMusicContext context) : base(context)
        {
            _context = context;
        }

        public List<Album> GetAlbumsWithSinger()
        {
            return _context.Albums.Include(x=>x.Singer).ToList();
        }
    }
}
