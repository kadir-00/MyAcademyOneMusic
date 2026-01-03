using Microsoft.AspNetCore.Http;

namespace OneMusic.WebUI.Models.UserModels
{
    public class ResultUserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string ImageURL { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string OldPassword { get; set; }

        public bool IsEmailConfirmed { get; set; }  
        public bool IsPhoneConfirmed { get; set; }

        public IFormFile CoverPhoto { get; set; }

    }
}
