using Business.Abstract;
using CoreLayer.Helpers;
using CoreLayer.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        SaveImageAttribute _saveImageAttribute = new SaveImageAttribute();
        [SaveImage]

        public IResult UploadImage(UploadImageDTO fileModel)
        { 
            _saveImageAttribute.Save(fileModel);
            return new SuccesfullResult();
        }
       //saveattribute
    }
}
            
