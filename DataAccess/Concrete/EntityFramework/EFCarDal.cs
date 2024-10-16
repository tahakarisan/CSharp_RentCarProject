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
        public List<CarDto> GetCarDto()
        {
            using (RentCarContext context = new RentCarContext())
            {

                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             join co in context.Colors
                             on c.ColorId equals co.Id
                             select new CarDto
                             {
                                 Id= c.Id,
                                 BrandName= b.BrandName,
                                 Description=c.Description,
                                 DailyPrice= c.DailyPrice,
                                 ModelYear = c.ModelYear,
                                 ColorName = co.ColorName
                             };
                return result.ToList();
            }
        }
    }





}
