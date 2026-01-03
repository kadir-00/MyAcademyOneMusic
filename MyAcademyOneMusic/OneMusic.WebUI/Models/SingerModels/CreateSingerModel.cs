using OneMusic.EntityLayer.Entities;

namespace OneMusic.WebUI.Models.SingerModels
{
    public class CreateSingerModel
    {
        public int SingerId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public IFormFile Image { get; set; }
    }
}
