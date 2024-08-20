
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
            if (DateTime.Now.Hour==16||DateTime.Now.Hour==8)
            {
                return new DataErrorResult<List<Car>>(_carDal.GetAll(),Messages.ListInMaintenance);
            }
            return new DataSuccesfullResult<List<Car>>(_carDal.GetAll(),Messages.CarListed);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new DataSuccesfullResult<List<Car>>(_carDal.GetAll(c => c.BrandId == id),Messages.BrandGetById);
        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
           return new DataSuccesfullResult<List<Car>>(_carDal.GetAll(c=>c.ColorId == id));   
        }
        public IResult Add(Car car)
        {
            if (car.Description.Length<10)
            {
                return new SuccesfullResult(Messages.CarAdded);
            }
            else
            {
                return new ErrorResult(Messages.CarNotAdded);
            }
        }     
        public IResult Delete(int id)
        {
            return new SuccesfullResult(Messages.CarDeleted);
        }
        public IDataResult<List<Car>> GetCarById(int id)
        {
            return new DataSuccesfullResult<List<Car>>(_carDal.GetAll(c=>c.Id==id),Messages.CarGetById);
        }
        public IResult Update(Car entity)
        {
            if (entity.Description.Length<10 && entity.DailyPrice<0)
            {
                return new ErrorResult(Messages.CarNotUpdated);
            }
            else
            {
                return new SuccesfullResult(Messages.CarUpdated);
            }
        }

    }
}
