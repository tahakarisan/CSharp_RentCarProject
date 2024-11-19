using CoreLayer.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IResult UploadImage(UploadImageDTO fileModel);
        IResult DeleteImage(int id);
        IResult UpdateImage(UploadImageDTO fileModel);
        IDataResult<List<CarImage>> GetAll();
        IDataResult<List<CarImage>> GetImagesByCarId(int carId);
  
    }
}
