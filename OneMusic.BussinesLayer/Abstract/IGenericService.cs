using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BussinesLayer.Abstarct
{
    public interface IGenericService<T> where T : class
    {
        // CRUD Islemlerini yapacagimiz sayfa 
        List<T> TGetList();

        T TGetById(int id);

        void TCreate(T entity);

        void TUpdate(T entity);

        void TDelete(int id);
    }
}
