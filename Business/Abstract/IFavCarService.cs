using CoreLayer.Entities.Concrete;
using CoreLayer.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IFavCarService
    {
        IDataResult<List<FavCarDto>> GetFavCarsByUserId(int userId);
        IResult Add(FavCar favCar);
        IResult Delete(int favCarId, int userId);
        IResult Update(FavCar favCar);
        IDataResult<List<FavCar>> GetAll();
    }
}
