using CoreLayer.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IResult UploadImage(CarImage carImage);
    }
}
