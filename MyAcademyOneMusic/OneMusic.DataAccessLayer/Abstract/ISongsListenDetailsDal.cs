using OneMusic.DataAccessLayer.Models;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.DataAccessLayer.Abstract
{
    public interface ISongsListenDetailsDal : IGenericDal<SongsListenDetails>
    {
        int countByListenedArtist(int id);

        bool IsActive(int id,int songId);


        List<SongsListenDetails> getMostListenSingers();

    }
}
