using CoreLayer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IFavCarDal:IEntityRepository<FavCar>
    {
        List<FavCarDto> GetAllByUserId(int userId);
    }
}
