using System.ComponentModel.DataAnnotations;

namespace OneMusic.WebUI.Models.SongModels
{
    public class UpdateSongViewModel
    {
        public int SongId { get; set; }
        [Required(ErrorMessage = "Şarkı adı boş geçilemez")]
        public string SongName { get; set; }
        public string? SongUrl { get; set; }
        [Required(ErrorMessage = "Lütfen albüm seçiniz")]
        public int AlbumId { get; set; }

        public IFormFile SongFile { get; set; }
    }
}
