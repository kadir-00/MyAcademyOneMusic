using OneMusic.BussinesLayer.Abstarct;
using OneMusic.DataAccesLayer.Abstarct;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BussinesLayer.Concreate
{
    public class MessageManager(IMessageDal _messageDal) : IMessageService
    {
        

        public void TCreate(Message entity)
        {
            _messageDal.Create(entity);
        }

        public void TDelete(int id)
        {
            _messageDal.Delete(id);
        }

        public Message TGetById(int id)
        {
           return _messageDal.GetById(id);
        }

        public List<Message> TGetList()
        {
           return _messageDal.GetList();
        }

        public void TUpdate(Message entity)
        {
            _messageDal.Update(entity);
        }
    }
}
