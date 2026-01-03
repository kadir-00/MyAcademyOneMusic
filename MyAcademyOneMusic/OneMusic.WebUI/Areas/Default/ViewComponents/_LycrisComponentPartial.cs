using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OneMusic.WebUI.Areas.Default.Models;
using OneMusic.WebUI.Models.ApiModels;

namespace OneMusic.WebUI.Areas.Default.ViewComponents
{
    public class _LycrisComponentPartial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string albumName = TempData["ResultAlbumName"].ToString();
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://genius-song-lyrics1.p.rapidapi.com/search/?q={albumName}&per_page=1&page=1"),
                Headers =
                      {
                    { "X-RapidAPI-Key", "your rapid api key" },
                    { "X-RapidAPI-Host", "genius-song-lyrics1.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var jsondata = JsonConvert.DeserializeObject<ResultApiViewModel>(body);

                var result2 = jsondata.hits[0].result.id;


                var client2 = new HttpClient();
                var request2 = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://genius-song-lyrics1.p.rapidapi.com/song/lyrics/?id={result2}"),
                    Headers =
                {
                    { "X-RapidAPI-Key", "your rapid api key" },
                    { "X-RapidAPI-Host", "genius-song-lyrics1.p.rapidapi.com" },
                },
                };
                using (var response2 = await client2.SendAsync(request2))
                {
                    response2.EnsureSuccessStatusCode();
                    var body2 = await response2.Content.ReadAsStringAsync();
                    var Lycris = JsonConvert.DeserializeObject<LycrisViewModel>(body2);

                    if (Lycris == null)
                    {
                        return View();
                    }
                    else
                    {
                        TestViewModel testViewModel = new TestViewModel()
                        {
                            Lyrnics = Lycris.lyrics.lyrics.body.html,
                        };
                        return View(testViewModel);

                    }

                }
            }
        }

    }
}


