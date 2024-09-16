using CoreLayer.DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFCarDal : EFEntityRepositoryBase<Car, RentCarContext>, ICarDal
    {
        public Car Get(Expression<Func<Car, bool>> filter)
        {
            using (RentCarContext context = new RentCarContext())
            {
                return context.Set<Car>().FirstOrDefault(filter);
            }
        }
  
        public void GetCarDto()
        {
            using (RentCarContext context = new RentCarContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             select new CarDto
                             {
                                 DailyPrice = c.DailyPrice,
                                 Id = c.Id,
                                 Description = c.Description,
                                 BrandId = Convert.ToString(b.Id)
                             };
                result.ToList();
            }


        }
    }





}
