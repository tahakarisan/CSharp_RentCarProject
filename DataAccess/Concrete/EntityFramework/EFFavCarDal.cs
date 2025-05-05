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
                             join cImage in context.CarImages
                             on c.Id equals cImage.CarId
                             into carImages
                             from image in carImages.DefaultIfEmpty()
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
                                 ImagePath = image.ImagePath == null ? "5c9e290c-1079-4bcf-8913-a97da764e092.jpg" : image.ImagePath.Remove(0, "C:\\Users\\SoftwareHP\\Desktop\\ReCap\\WebAPI\\wwwroot\\images\\".Length)

                             };
                return result.ToList();
            }
        }
    }
}
