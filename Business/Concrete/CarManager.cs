
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constant;
using Business.ValidationRules.AddValidation;
using Business.ValidationRules.UpdateValidation;
using CoreLayer.Aspects.Autofac;
using CoreLayer.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        IUserService _userService;
        public CarManager(ICarDal carDal, IUserService userService)
        {
            _carDal = carDal;
            _userService = userService;
        }

        public IDataResult<List<CarDto>> GetDetail()
        {
            if (DateTime.Now.Hour == 3 || DateTime.Now.Hour == 5)
            {
                return new ErrorDataResult<List<CarDto>>(Messages.ListInMaintenance);
            }
            return new SuccesfulDataResult<List<CarDto>>(data: _carDal.GetCarDto(), Messages.CarListed);
        }


        //[CacheAspect]
        public IDataResult<List<CarDto>> GetAll()
        {
            if (DateTime.Now.Hour == 3 || DateTime.Now.Hour == 5)
            {
                return new ErrorDataResult<List<CarDto>>(Messages.ListInMaintenance);
            }
            return new SuccesfulDataResult<List<CarDto>>(data:_carDal.GetCarDto(), Messages.CarListed);
        }

        [CacheAspect]
        public IDataResult<List<CarDto>> GetCarsByBrandId(int brandId)
        {
            var result = _carDal.GetCarByBrandDto(brandId);
            if (result==null)
            {
                return new ErrorDataResult<List<CarDto>>(message:"Böyle bir araba yok");
            }
            return new SuccesfulDataResult<List<CarDto>>(data:result);
        }

        [CacheAspect]
        public IDataResult<List<CarDto>> GetCarsByColorId(int id)
        {
            return new SuccesfulDataResult<List<CarDto>>(data: _carDal.GetCarByColorId(id));
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(CarValidator))]
        //[PerformanceAspect(5)]
        public IResult Add(Car car)
        {
            return _carDal.Add(car) ? new SuccesfullResult(Messages.CarAdded) : new ErrorResult();
        }
        public IResult Delete(int id)
        {
            var result = _carDal.FirstOrDefault(c => c.Id == id);
            if (result == null)
            {
                return new ErrorResult("Araba silinemedi");
            }
            _carDal.Delete(id);
            return new SuccesfullResult("Araba Silindi");
        }
        [CacheAspect]
        public IDataResult<List<Car>> GetCarById(int id)
        {
            return new SuccesfulDataResult<List<Car>>(data: _carDal.GetAll(c => c.Id == id), Messages.CarGetById);
        }

        [ValidationAspect(typeof(UpdateCarValidator))]
        public IResult Update(Car car)
        {
            var result = _carDal.FirstOrDefault(c => c.Id == car.Id);
            if (result != null)
            {
                _carDal.Update(car);
                return new SuccesfullResult("Araba Güncellendi");
            }
            return new ErrorResult("Güncellemek istediğiniz araba bulunamadı");
        }
        private IResult IsDescriptionInValid(Car car)
        {
            var result = _carDal.GetAll(c => c.Description == car.Description).Any();
            if (result)
            {
                return new ErrorResult("Bu açıklamaya sahip araba mevcut");
            }
            return new SuccesfullResult();
        }
        public IDataResult<Car> GetById(int carId)
        {
            // ID'ye göre aracı getir
            var car = _carDal.FirstOrDefault(c => c.Id == carId);

            if (car == null)
            {
                return new ErrorDataResult<Car>("Araba bulunamadı.");  // Eğer araç bulunamazsa hata döndür
            }
            _carDal.FirstOrDefault(c => c.Id == carId);

            return new SuccesfulDataResult<Car>(data: car);  // Başarılı bir şekilde araç bulundu
        }

        public IDataResult<List<CarDto>> GetDetailByCarId(int carId)
        {
            return new SuccesfulDataResult<List<CarDto>>(data: _carDal.GetCarDetailByCarId(carId));
        }

        public IDataResult<List<CarDto>> FilterCars(int brandId,int colorId)
        {
            var cars = _carDal.FilterCars(brandId,colorId);

            if (cars.Count>0)
            {
                return new SuccesfulDataResult<List<CarDto>>(data:cars);
            }

            return new ErrorDataResult<List<CarDto>>(message:"Listelenemedi");

        }
    }

}
