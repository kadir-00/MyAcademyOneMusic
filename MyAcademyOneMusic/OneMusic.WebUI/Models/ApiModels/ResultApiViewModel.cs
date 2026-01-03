namespace OneMusic.WebUI.Models.ApiModels
{
    public class ResultApiViewModel
    {
        public Hit[] hits { get; set; }

        public class Hit
        {
            public object[] highlights { get; set; }
            public string index { get; set; }
            public string type { get; set; }
            public Result result { get; set; }
        }

        public class Result
        {
            public string _type { get; set; }
            public int id { get; set; }

        }



    }
}
