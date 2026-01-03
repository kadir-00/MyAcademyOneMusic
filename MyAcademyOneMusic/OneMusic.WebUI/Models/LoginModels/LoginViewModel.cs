using System.ComponentModel.DataAnnotations;

namespace OneMusic.WebUI.Models.LoginModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Kullanıcı adınızı yazınız")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Şifrenizi giriniz")]
        public string Password { get; set; }
    }
}
