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
    public class EFMessageDal : GenericRepository<Message>, IMessageDal

    {
        public EFMessageDal(OneMusicContext context) : base(context)
        {
        }
    }
}
