using CoreLayer.DataAccess;
using CoreLayer.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using System;
using System.Collections.Generic;
using System.IO;
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
                             group new { fc,c,br, co, image } by c.Id into grouped
                             select new FavCarDto
                             {
                                 Id = grouped.First().fc.Id,
                                 UserId = grouped.First().fc.UserId,
                                 CarId = grouped.First().c.Id,
                                 ColorName = grouped.First().co.ColorName,
                                 BrandName = grouped.First().br.BrandName,
                                 ModelYear = grouped.First().c.ModelYear,
                                 DailyPrice = grouped.First().c.DailyPrice,
                                 Description = grouped.First().c.Description,
                                 ImagePath = grouped.FirstOrDefault().image != null && !string.IsNullOrEmpty(grouped.FirstOrDefault().image.ImagePath)
                                             ? Path.GetFileName(grouped.FirstOrDefault().image.ImagePath)
                                             : "5c9e290c-1079-4bcf-8913-a97da764e092.jpg"

                             };
                return result.ToList();
            }
        }
    }
}
