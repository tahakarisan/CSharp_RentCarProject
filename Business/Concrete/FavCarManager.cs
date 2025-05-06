using Business.Abstract;
using CoreLayer.Entities.Concrete;
using CoreLayer.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class FavCarManager : IFavCarService
    {
        IFavCarDal _favCarDal;
        public FavCarManager(IFavCarDal favCarDal)
        {
            _favCarDal = favCarDal;
        }

        public IResult Add(FavCar favCar)
        {
            var result = _favCarDal.Add(favCar);
            if (result)
            {
                return new SuccesfullResult("Favori Araba Eklendi");
            }
            else
            {
                return new ErrorResult("Favori Araba Eklenemedi");
            }
        }

        public IResult Delete(int favCarId,int userId)
        {
            var query = _favCarDal.FirstOrDefault(c => c.UserId == userId && c.Id==favCarId);
            var result = _favCarDal.Delete(query.Id);
            if (result)
            {
                return new SuccesfullResult("Favori Araba Silindi");
            }
            else
            {
                return new ErrorResult("Favori Araba Silinemedi");
            }
        }

        public IDataResult<List<FavCar>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<FavCarDto>> GetFavCarsByUserId(int userId)
        {
            var query = _favCarDal.GetAllByUserId(userId);
            if (query.Count > 0)
            {
                return new SuccesfulDataResult<List<FavCarDto>>(query);
            }
            else
            {
                return new ErrorDataResult<List<FavCarDto>>(query);
            }
        }

        public IResult Update(FavCar favCar)
        {
            if (_favCarDal.Update(favCar))
            {
                return new SuccesfullResult("Favori Araba Güncellendi");
            }
            else
            {
                return new ErrorResult("Favori Araba Güncellenemedi");
            }
        }
    }
}
