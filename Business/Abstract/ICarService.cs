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
        IDataResult<List<Car>> GetCarsByColorId(int id);
        IResult Delete(int id);
        IDataResult<List<Car>> GetCarsByBrandId(int id);
        IResult Update(Car car);

    }
}
