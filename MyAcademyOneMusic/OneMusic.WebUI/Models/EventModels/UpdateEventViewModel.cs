namespace OneMusic.WebUI.Models.EventModels
{
    public class UpdateEventViewModel
    {
        public int EventID { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string ImageURL { get; set; }
        public DateTime Date { get; set; }

        public IFormFile FormFile { get; set; }
    }
}
