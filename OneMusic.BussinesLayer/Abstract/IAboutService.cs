using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BussinesLayer.Abstarct
{
    // Burada implement interface yapmadik neden? Cunku, burasida bir interfae baska bir interface'den miras aldigi zaman 
    // gerek yok
    public interface IAboutService : IGenericService<About>
    {
    }
}
