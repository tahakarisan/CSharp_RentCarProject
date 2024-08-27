
using Business.Abstract;
using Business.Constant;
using CoreLayer.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
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
        public IResult Add(Car car)
        {
            if (car.Description.Length < 5)
            {
                return new ErrorResult(Messages.CarNotAdded);
            }
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

        public IResult Update(Car car)
        {
            if (!_carDal.GetAll(c => c.Id == car.Id).Any())
            {
                return new ErrorResult("Araba güncellenemedi");
            }
            _carDal.Update(car);
            return new SuccesfullResult("Araba Güncellendi");
        }
    }
}
