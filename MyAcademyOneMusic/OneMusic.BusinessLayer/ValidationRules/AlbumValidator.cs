using FluentValidation;
using OneMusic.BusinessLayer.Models.Album;

namespace OneMusic.BusinessLayer.ValidationRules
{
    public class AlbumValidator : AbstractValidator<CreateAlbumViewModel>
    {
        public AlbumValidator()
        {
            RuleFor(x => x.AlbumName).NotEmpty().WithMessage("Albüm adı boş bırakılamaz");
            RuleFor(x => x.Price).Must(IsZeroDecimal).WithMessage("Albüm fiyatı 0'dan büyük olmalıdır");
            RuleFor(x => x.Image.FileName).Must(CheckExtension).WithMessage("Seçtiğiniz dosya uzantısı desteklenmiyor lütfen görsel uzantılarından (.jpg - .png - .jpeg) birini seçin.");

        }
        private bool IsZeroInt(int Id)
        {
            return Id != 0;
        }
        private bool IsZeroDecimal(decimal Price)
        {
            return Price != 0;
        }
        private bool CheckExtension(string fileName)
        {
            var ex = Path.GetExtension(fileName);
            if (ex == ".png" || ex == "jpg" || ex == ".jpeg")
            {
                return true;
            }
            return false;
        }

    }
}
