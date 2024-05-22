using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.DataAccesLayer.Abstarct
{
    public interface IAlbumDal : IGenericDal<Album> 
    {
      

        List<Album> GetAlbumsByArtist(int id);
    }
}
