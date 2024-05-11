using OneMusic.DataAccesLayer.Abstarct;
using OneMusic.DataAccesLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.DataAccesLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class.
    {
        private readonly OneMusicContext _context;

        public GenericRepository(OneMusicContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<T> GetList()
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
