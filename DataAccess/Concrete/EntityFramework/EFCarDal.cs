using Castle.Components.DictionaryAdapter.Xml;
using CoreLayer.DataAccess;
using CoreLayer.Entities.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public List<CarDto> GetCarDto()
        {
            using (RentCarContext context = new RentCarContext())
            {

                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.Id
                             join co in context.Colors on c.ColorId equals co.Id
                             join carImage in context.CarImages
                                 on c.Id equals carImage.CarId into carImages
                             from image in carImages.DefaultIfEmpty()
                             select new CarDto
                             {
                                 Id = c.Id,
                                 BrandName = b.BrandName,
                                 Description = c.Description,
                                 DailyPrice = c.DailyPrice,
                                 ModelYear = c.ModelYear,
                                 ColorName = co.ColorName,
                                 ImagePath = image != null ? image.ImagePath.Remove(0, "C:\\Users\\SoftwareHP\\Desktop\\ReCap\\WebAPI\\wwwroot\\images\\".Length) : "5c9e290c-1079-4bcf-8913-a97da764e092.jpg"
                             };

                return result.ToList();
            }
        }
        public List<CarDto> GetCarByBrandDto(int brandId)
        {
            using (RentCarContext context = new RentCarContext())
            {

                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             join co in context.Colors
                             on c.ColorId equals co.Id
                             join carImage in context.CarImages
                                on c.Id equals carImage.CarId into carImages
                             from image in carImages.DefaultIfEmpty()
                             where c.BrandId == brandId
                             select new CarDto
                             {
                                 Id = c.Id,
                                 BrandName = b.BrandName,
                                 Description = c.Description,
                                 DailyPrice = c.DailyPrice,
                                 ModelYear = c.ModelYear,
                                 ImagePath = image != null ? image.ImagePath.Remove(0, "C:\\Users\\SoftwareHP\\Desktop\\ReCap\\WebAPI\\wwwroot\\images\\".Length) : "5c9e290c-1079-4bcf-8913-a97da764e092.jpg",
                                 ColorName = co.ColorName

                             };
                return result.ToList();
            }
        }
        public List<CarDto> GetCarByColorId(int colorId)
        {
            using (var context = new RentCarContext())
            {
                var result = from c in context.Cars
                             join co in context.Colors
                             on c.ColorId equals co.Id
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             join carImage in context.CarImages
                             on c.Id equals carImage.CarId into
                             carImages
                             from image in carImages.DefaultIfEmpty()
                             where c.ColorId == colorId
                             select new CarDto
                             {
                                 Id = c.Id,
                                 BrandId = c.BrandId,
                                 BrandName = b.BrandName,
                                 ColorName = co.ColorName,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 ModelYear = c.ModelYear,
                                 ImagePath = image.ImagePath == null ? "5c9e290c-1079-4bcf-8913-a97da764e092.jpg" : image.ImagePath.Remove(0, "C:\\Users\\SoftwareHP\\Desktop\\ReCap\\WebAPI\\wwwroot\\images\\".Length)
                             };
                return result.ToList();
            }
        }

        public List<CarDto> GetCarDetailByCarId(int carId)
        {
            using (var context = new RentCarContext())
            {

                var result = from c in context.Cars
                             join co in context.Colors
                             on c.ColorId equals co.Id
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             join cImage in context.CarImages
                             on c.Id equals cImage.CarId
                             into carImages
                             from image in carImages.DefaultIfEmpty()
                             where c.Id == carId
                             select new CarDto
                             {
                                 Id = c.Id,
                                 BrandId = c.BrandId,
                                 BrandName = b.BrandName,
                                 ColorName = co.ColorName,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 ModelYear = c.ModelYear,
                                 ImagePath = image.ImagePath == null ? "5c9e290c-1079-4bcf-8913-a97da764e092.jpg" : image.ImagePath.Remove(0, "C:\\Users\\SoftwareHP\\Desktop\\ReCap\\WebAPI\\wwwroot\\images\\".Length)
                             };

                return result.ToList();


            }
        }

        public List<CarDto> FilterCars(int brandId, int colorId)
        {
            using (var context = new RentCarContext())
            {
                var result = from c in context.Cars
                             join co in context.Colors
                             on c.ColorId equals co.Id
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             join cImage in context.CarImages
                             on c.Id equals cImage.CarId
                             into carImages
                             from image in carImages.DefaultIfEmpty()
                             where c.BrandId == brandId && c.ColorId == colorId
                             select new CarDto
                             {
                                 Id = c.Id,
                                 BrandId = c.BrandId,
                                 BrandName = b.BrandName,
                                 ColorName = co.ColorName,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 ModelYear = c.ModelYear,
                                 ImagePath = image.ImagePath == null ? "5c9e290c-1079-4bcf-8913-a97da764e092.jpg" : image.ImagePath.Remove(0, "C:\\Users\\SoftwareHP\\Desktop\\ReCap\\WebAPI\\wwwroot\\images\\".Length)

                             };

                return result.ToList();


            }
        }
    }





}
