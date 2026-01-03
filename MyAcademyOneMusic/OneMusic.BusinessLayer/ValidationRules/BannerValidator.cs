using FluentValidation;
using OneMusic.BusinessLayer.Models.Banner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BusinessLayer.ValidationRules
{
    public class BannerValidator : AbstractValidator<CreateBannerViewModel>
    {
        public BannerValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Bu alanı doldurun");
            RuleFor(x => x.Title).MaximumLength(50).WithMessage("Başlık maksimum 50 karakter içermelidir.");
            RuleFor(x => x.Title).MinimumLength(5).WithMessage("Başlık maksimum 5 karakter içermelidir.");


            RuleFor(x => x.SubTitle).NotEmpty().WithMessage("Bu alanı doldurun");
            RuleFor(x => x.SubTitle).MaximumLength(50).WithMessage("Alt başlık maksimum 50 karakter içermelidir.");
            RuleFor(x => x.SubTitle).MinimumLength(5).WithMessage("Alt Başlık maksimum 5 karakter içermelidir.");



            RuleFor(x => x.coverPhoto.FileName).Must(CheckExtension).WithMessage("Seçtiğiniz dosya uzantısı desteklenmiyor lütfen görsel uzantılarından (.jpg - .png - .jpeg) birini seçin.");
        }
        private bool CheckExtension(string fileName)
        {
            var ex = Path.GetExtension(fileName);
            if (ex == ".png" || ex == ".jpg" || ex == ".jpeg")
            {
                return true;
            }
            return false;
        }
    }

}
