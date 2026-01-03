using Microsoft.AspNetCore.Http;

namespace OneMusic.WebUI.Models.EventModels
{
    public class CreateEventViewModel
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public string ImageURL { get; set; }
        public DateTime Date { get; set; }

        public IFormFile FormFile { get; set; }
    }
}
