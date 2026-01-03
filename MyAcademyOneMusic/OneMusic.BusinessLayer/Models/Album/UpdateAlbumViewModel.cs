using Microsoft.AspNetCore.Http;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BusinessLayer.Models.Album
{
    public class UpdateAlbumViewModel
    {
        public int AlbumId { get; set; }
        public string AlbumName { get; set; }
        public string CoverImage { get; set; }
        public decimal Price { get; set; }

        public int? AppUserId { get; set; }


        public IFormFile Image { get; set; }
    }
}
