namespace OneMusic.WebUI.Areas.Artist.Models
{
    public class EditArtistViewModel
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ImageURL { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile ImageFile { get; set; }
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public bool IsEmailConfirmed { get; set; }
    }
}
