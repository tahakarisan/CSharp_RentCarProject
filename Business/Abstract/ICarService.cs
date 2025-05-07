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
        IDataResult<List<SoftCarDto>> GetAll();
        IDataResult<Car> GetById(int carId);
        IDataResult<List<CarDto>> GetCarsByColorId(int id);
        IResult Delete(int id);
        IDataResult<List<SoftCarDto>> GetCarsByBrandId(int id);
        IResult Update(Car car);
        IDataResult<List<ImageCarDto>> GetDetail();
        IDataResult<List<ImageCarDto>> GetDetailByCarId(int carId);
        IDataResult<List<CarDto>> FilterCars(int brandId,int colorId);

    }
}
