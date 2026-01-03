using OneMusic.DataAccessLayer.Models;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BusinessLayer.Abstract
{
    public interface ISongsListenDetailsService : IGenericService<SongsListenDetails>
    {
        int TcountByListenedArtist(int id);
        bool TIsActive(int id, int songId);

        List<SongsListenDetails> TgetMostListenSingers();
    }
}
