using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.DataAccessLayer.Abstract
{
    public interface IAlbumDal : IGenericDal<Album>
    {
        List<Album> getAlbumByArtist(int id);

        List<Album> getAlbumListWithArtist();
        List<Album> getAlbumWithArtistByStatusFalseList();
        List<Album> getAlbumWithArtistRejectLists();
        List<Album> getListAwatingApprovalAlbums(int id);

        List<Album> getListAlbumWithCategoryAndArtist(string category, string artist);

        List<Album> getListAlbumWithCategory(string category);

        List<Album> getListAlbumWithArtist(string artist);

        Album getAlbumByIDWithAppUser(int id);


        List<Album> getRandomAlbumWithArtist();



        int AlbumCount(int id);
        int AlbumCountByWaiting(int id);
        string ExpensiveAlbumName(int id);

        Album GetAlbumByIdWithSongs(int id);
        List<Album> GetLast6Albums();
        List<Album> GetAlbumsOrderedByDate(int maxPerArtist, int totalCount);
    }
}
