using Microsoft.AspNetCore.Http;

namespace OneMusic.WebUI.ImageSettings
{
    public static class ImageSetting
    {
        public static string CreateImage(IFormFile formFile, string path)
        {
            var ex = Path.GetExtension(formFile.FileName);
            var imageName = Guid.NewGuid() + ex;
            var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/" + path + "/", imageName);
            var stream = new FileStream(location, FileMode.Create);
            formFile.CopyTo(stream);
            stream.Close();
            stream.Dispose();
            return "/Images/" + path + "/" + imageName;
        }

        public static void DeleteImage(string imagePath)
        {
            var location = "wwwroot" + imagePath;
            if (System.IO.File.Exists(location))
            {
                System.IO.File.Delete(location);
            }
        }

        public static string CreateSong(IFormFile formFile)
        {
            var ex = Path.GetExtension(formFile.FileName);
            var songname = Guid.NewGuid() + ex;
            var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Songs/", songname);
            var stream = new FileStream(location, FileMode.Create);
            formFile.CopyTo(stream);
            stream.Close();
            stream.Dispose();
            return "/Songs/" + songname;
        }
    }
}
