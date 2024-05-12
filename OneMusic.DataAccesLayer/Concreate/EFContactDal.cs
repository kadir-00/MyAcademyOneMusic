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
    public class EFContactDal : GenericRepository<Contact>, IContactDal
    {
        public EFContactDal(OneMusicContext context) : base(context)
        {
        }
    }
}
