using Business.Abstract;
using CoreLayer.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }
        public IResult UploadImage(CarImage carImage)
        {
            var result = _carImageDal.Add(carImage);
            if (result)
            {
                return new SuccesfullResult();
            }
            return new ErrorResult();
            
        }
    }
}
