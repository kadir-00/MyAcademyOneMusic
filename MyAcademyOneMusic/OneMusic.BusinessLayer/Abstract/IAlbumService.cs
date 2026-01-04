using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BusinessLayer.Abstract
{
    public interface IAlbumService : IGenericService<Album>
    {
        List<Album> TgetAlbumByArtist(int id);
        List<Album> TgetAlbumListWithArtist();
        int TAlbumCount(int id);
        int TAlbumCountByWaiting(int id);
        string TExpensiveAlbumName(int id);

        Album TgetAlbumByIDWithAppUser(int id);

        List<Album> TgetAlbumWithArtistByStatusFalseList();
        List<Album> TgetAlbumWithArtistRejectLists();
        List<Album> TgetListAwatingApprovalAlbums(int id);
        List<Album> TgetRandomAlbumWithArtist();

        List<Album> TgetListAlbumWithCategoryAndArtist(string category, string artist);

        List<Album> TgetListAlbumWithCategory(string category);

        List<Album> TgetListAlbumWithArtist(string artist);

        Album TGetAlbumByIdWithSongs(int id);
        List<Album> TGetLast6Albums();
        List<Album> TGetAlbumsOrderedByDate(int maxPerArtist, int totalCount);
    }
}
