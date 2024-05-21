using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BussinesLayer.Abstarct
{
    public interface IAlbumService:IGenericService<Album>
    {
        List<Album> TGetAlbumsWithSinger();

        List<Album> TGetAlbumsByArtist(int id);
    }
}
