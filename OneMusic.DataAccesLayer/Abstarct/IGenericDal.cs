using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.DataAccesLayer.Abstarct
{
    internal interface IGenericDal<T> where T : class
    {
        // CRUD Islemlerini yapacagimiz sayfa 
        List<T> GetList();

        T GetById(int id);
        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
