
using Business.Abstract;
using Business.Constant;
using Business.ValidationRules.AddValidation;
using Business.ValidationRules.UpdateValidation;
using CoreLayer.Aspects.Autofac;
using CoreLayer.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour == 16 || DateTime.Now.Hour == 8)
            {
                return new DataErrorResult<List<Car>>(Messages.ListInMaintenance);
            }
            return new DataSuccesfullResult<List<Car>>(_carDal.GetAll(), Messages.CarListed);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new DataSuccesfullResult<List<Car>>(_carDal.GetAll(c => c.BrandId == id), Messages.BrandGetById);
        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new DataSuccesfullResult<List<Car>>(_carDal.GetAll(c => c.ColorId == id));
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            _carDal.Add(car);
            return new SuccesfullResult(Messages.CarAdded);
            
        }
        public IResult Delete(int id)
        {
            if (!_carDal.GetAll(c=>c.Id==id).Any())
            {
                return new ErrorResult("Araba silinemedi");
            }
            _carDal.Delete(id);
            return new SuccesfullResult("Araba Silindi");
        }
        public IDataResult<List<Car>> GetCarById(int id)
        {
            return new DataSuccesfullResult<List<Car>>(_carDal.GetAll(c => c.Id == id), Messages.CarGetById);
        }

        [ValidationAspect(typeof(UpdateCarValidator))]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccesfullResult("Araba Güncellendi");
        }
        private IResult IsDescriptionInValid(Car car)
        {
            var result = _carDal.GetAll(c=>c.Description==car.Description).Any();
            if (result)
            {
                return new ErrorResult("Bu açıklamaya sahip araba mevcut");
            }
            return new SuccesfullResult();
        }
        public IDataResult<Car> GetById(int carId)
        {
            // ID'ye göre aracı getir
            var car = _carDal.Get(c => c.Id == carId);

            if (car == null)
            {
                return new DataErrorResult<Car>("Araba bulunamadı.");  // Eğer araç bulunamazsa hata döndür
            }

            return new DataSuccesfullResult<Car>(car, "Araba başarıyla bulundu.");  // Başarılı bir şekilde araç bulundu
        }


    }

}
