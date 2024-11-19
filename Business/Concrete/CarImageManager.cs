using Business.Abstract;
using Business.Helpers.FileHelper;
using CoreLayer.Aspects.Autofac;
using CoreLayer.Utilities.Business;
using CoreLayer.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }
        public IDataResult<List<CarImage>> GetAll()
        {
            var result = _carImageDal.GetCarImage();
            if (result.Count > 0)
            {
                return new SuccesfulDataResult<List<CarImage>>(data:_carImageDal.GetCarImage(), "Resimler listelendi");
            }
            return new ErrorDataResult<List<CarImage>>("Resimler listelenemedi");

        }
        public IResult DeleteImage(int id)
        {
            var result = _carImageDal.GetAll(c=>c.Id==id).Any();
            if (result)
            {
                _carImageDal.Delete(id);
                return new SuccesfullResult("Silindi");
            }
            return new ErrorResult("Silinemedi");
        }
        public IResult UpdateImage(UploadImageDTO fileModel)
        {
            var oldImage = _carImageDal.GetAll(c=>c.CarId == fileModel.CarId);
            var oldImageData = oldImage.FirstOrDefault();
            if (oldImage.Count>0)
            {
                var saveImageResult = FileHelper.AddImage(fileModel.RequestedFormFile);

                if (saveImageResult.Data == null || saveImageResult.Success == true)
                {
                    CarImage carImage = new CarImage
                    {
                        Id = oldImageData.Id,
                        CarId = fileModel.CarId,
                        ImagePath = saveImageResult.Data,
                        Name = fileModel.Name
                    };
                    IResult result = BusinessRules.Run(ImageLimit(carImage));
                    _carImageDal.Update(carImage);
                    return new SuccesfullResult("Güncellendi");
                }
                if (!saveImageResult.Success)
                {
                    return new ErrorResult("Güncellenemedi");
                }

            }
            return new ErrorResult("Güncellenemedi");

        }

        public IResult UploadImage(UploadImageDTO fileModel)
        {
            if (fileModel != null)
            {
                var saveImageResult = FileHelper.AddImage(fileModel.RequestedFormFile);

                if (saveImageResult.Data == null || saveImageResult.Success == true)
                {
                    CarImage carImage = new CarImage
                    {
                        CarId = fileModel.CarId,
                        ImagePath = saveImageResult.Data,
                        Name = fileModel.Name
                    };
                    IResult result = BusinessRules.Run(ImageLimit(carImage));
                    _carImageDal.Add(carImage);
                    return new SuccesfullResult("Eklendi");
                }
                if (!saveImageResult.Success)
                {
                    return new ErrorResult("Eklenemedi.");
                }

            }
            return new ErrorResult("Model boş olduğu için Eklenemedi");

        }
        private IResult ImageLimit(CarImage carImage)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carImage.CarId).Count;
            if (result > 5)
            {
                return new ErrorResult("Resim sınırına ulaşıldı");
            }
            return new SuccesfullResult();
        }

        public IDataResult<List<CarImage>> GetImagesByCarId(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId);
            if (result != null)
            {
                return new SuccesfulDataResult<List<CarImage>>(data:result);
            }
            return new ErrorDataResult<List<CarImage>>("Araba mevcut değil");
        }
    }
}

