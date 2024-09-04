using CoreLayer.DataAccess;
using DataAccess.Abstract;
using Entities;
using Entities.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.PortableExecutable;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Text;

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
                             on c.BrandId equals b.BrandId
                             select new CarDto
                             {
                                 DailyPrice = c.DailyPrice,
                                 Id = c.Id,
                                 Description = c.Description,
                                 BrandId = Convert.ToString(b.BrandId)
                             };
                result.ToList();
            }


        }
    }





}
