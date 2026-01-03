using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.EntityLayer.Entities
{
    public class Album
    {
        public int AlbumId { get; set; }
        public string AlbumName { get; set; }
        public string CoverImage { get; set; }
        public string Price { get; set; }

        public List<Song> Songs { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
