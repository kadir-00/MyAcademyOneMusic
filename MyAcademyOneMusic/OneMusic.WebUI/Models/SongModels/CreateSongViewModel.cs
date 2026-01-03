using OneMusic.EntityLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace OneMusic.WebUI.Models.SongModels
{
    public class CreateSongViewModel
    {
        [Required(ErrorMessage ="Şarkı adı boş geçilemez")]
        public string SongName { get; set; }
        public string? SongUrl { get; set; }
        [Required(ErrorMessage = "Lütfen albüm seçiniz")]
        public int AlbumId { get; set; }

        [Required(ErrorMessage = "Lütfen şarkınızı seçiniz")]
        public IFormFile SongFile { get; set; }

    }
}
