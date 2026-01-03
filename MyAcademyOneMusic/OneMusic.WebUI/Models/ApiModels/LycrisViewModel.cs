namespace OneMusic.WebUI.Models.ApiModels
{
    public class LycrisViewModel
    {

        public Lyrics lyrics { get; set; }


        public class Lyrics
        {
            public Lyrics1 lyrics { get; set; }

        }

        public class Lyrics1
        {
            public Body body { get; set; }
        }

        public class Body
        {
            public string html { get; set; }
        }




    }
}
