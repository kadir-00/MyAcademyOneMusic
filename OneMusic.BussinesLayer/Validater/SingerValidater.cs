using FluentValidation;
using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BussinesLayer.Validater
{
    public class SingerValidater : AbstractValidator<Singer>
    {
        public SingerValidater()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Sarkici Adi Bos Birakilamaz").MaximumLength(50).WithMessage("En fazla 50 Karakter").MinimumLength(4).WithMessage("Min 4 Karakter Yazilabilir");

            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Resim URL Degeri Bos Birakilamaz");
        }
    }
}
