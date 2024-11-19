using CoreLayer.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService
    {
        IResult Add(Car car);
        IDataResult<List<CarDto>> GetAll();
        IDataResult<Car> GetById(int carId);
        IDataResult<List<CarDto>> GetCarsByColorId(int id);
        IResult Delete(int id);
        IDataResult<List<CarDto>> GetCarsByBrandId(int id);
        IResult Update(Car car);
        IDataResult<List<CarDto>> GetDetail();
        IDataResult<List<CarDto>> GetDetailByCarId(int carId);

    }
}
