using CoreLayer.DataAccess;
using CoreLayer.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFFavCarDal:EFEntityRepositoryBase<FavCar,RentCarContext>,IFavCarDal
    {
        public List<FavCarDto> GetAllByUserId(int userId)
        {
            using (RentCarContext context = new RentCarContext())
            {
                var result = from fc in context.FavCars
                             join c in context.Cars on fc.CarId equals c.Id
                             join co in context.Colors on c.ColorId equals co.Id
                             join br in context.Brands on c.BrandId equals br.Id
                             where fc.UserId == userId
                             select new FavCarDto
                             {
                                 Id = fc.Id,
                                 UserId = fc.UserId,
                                 CarId = c.Id,
                                 ColorName = co.ColorName,
                                 BrandName = br.BrandName,
                                 ModelYear = c.ModelYear,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,

                             };
                return result.ToList();
            }
        }
    }
}
