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
        Car Get(Expression<Func<Car, bool>> filter);
        List<CarDto> GetCarDto();

    }
}
