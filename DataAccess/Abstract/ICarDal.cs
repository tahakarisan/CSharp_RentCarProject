using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICarDal : IEntityRepository<Car>
    {
        List<SoftCarDto> GetAllCar();
        Car Get(Expression<Func<Car, bool>> filter);
        List<ImageCarDto> GetCarDto();
        List<CarDto> GetCarByBrandDto(int brandId);
        List<CarDto> GetCarByColorId(int colorId);
        List<ImageCarDto> GetCarDetailByCarId(int carId);
        List<CarDto> FilterCars(int brandId,int colorId);

    }
}
