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
            if (fileModel?.RequestedFormFile == null || !fileModel.RequestedFormFile.Any())
                return new ErrorResult("Güncellenecek resim bulunamadı.");

            // 1. Eski resimleri sil
            var existingImages = _carImageDal.GetAll(c => c.CarId == fileModel.CarId);
            foreach (var image in existingImages)
            {
                _carImageDal.Delete(image.Id);
            }

            // 2. Yeni resimleri ekle
            foreach (var file in fileModel.RequestedFormFile)
            {
                var saveImageResult = FileHelper.AddImage(file);
                if (!saveImageResult.Success || saveImageResult.Data == null)
                {
                    return new ErrorResult("Yeni resim yüklenemedi: " + saveImageResult.Message);
                }

                var newImage = new CarImage
                {
                    CarId = fileModel.CarId,
                    ImagePath = saveImageResult.Data,
                    Name = fileModel.Name
                };

                var ruleResult = BusinessRules.Run(ImageLimit(newImage));
                if (ruleResult != null) return ruleResult;

                _carImageDal.Add(newImage);
            }

            return new SuccesfullResult("Resimler başarıyla güncellendi.");
        }


        public IResult UploadImage(UploadImageDTO fileModel)
        {
            if (fileModel?.RequestedFormFile == null || !fileModel.RequestedFormFile.Any())
            {
                return new ErrorResult("Model boş olduğu için eklenemedi");
            }

            foreach (var file in fileModel.RequestedFormFile)
            {
                var saveImageResult = FileHelper.AddImage(file); 

                if (!saveImageResult.Success || saveImageResult.Data == null)
                {
                    return new ErrorResult("Bir resim eklenemedi: " + saveImageResult.Message);
                }

                CarImage carImage = new CarImage
                {
                    CarId = fileModel.CarId,
                    ImagePath = saveImageResult.Data,
                    Name = fileModel.Name // İstersen dosya adına göre değiştirebiliriz
                };

                IResult ruleResult = BusinessRules.Run(ImageLimit(carImage));
                if (ruleResult != null) return ruleResult;

                _carImageDal.Add(carImage);
            }

            return new SuccesfullResult("Tüm resimler başarıyla eklendi.");
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

