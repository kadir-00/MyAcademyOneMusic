using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BussinesLayer.Validater
{
	public class CustomErrorDescriber : IdentityErrorDescriber
	{
		public override IdentityError PasswordTooShort(int length)
		{
			return new IdentityError
			{
				Description = "Sifre En az 6 Karakterden Olusmalidir."
			};
		}

		public override IdentityError PasswordRequiresDigit() // Rakam barindirmasi
		{
			return new IdentityError
			{
				Description = "sifre en az 1 rakam icermeli "
			};
		}

		public override IdentityError PasswordRequiresLower() // kucuk harf olmasi 
		{
			return new IdentityError
			{
				Description = "Sifre en az bir kucuk harf icermelidir."
			};
		}

		public override IdentityError PasswordRequiresUpper()
		{
			return new IdentityError
			{
				Description = "Sifre en az bir buyuk harf icermelidir."
			};
		}

		public override IdentityError PasswordRequiresNonAlphanumeric()
		{
			return new IdentityError
			{
				Description = "Sifre en az bir Ozel Karakter icermelidir."
			};
		}

	}
}
