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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
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
        public List<SoftCarDto> GetAllCar()
        {
            using (RentCarContext context = new RentCarContext())
            {
                var result = from car in context.Cars
                             join brand in context.Brands on car.BrandId equals brand.Id
                             join color in context.Colors on car.ColorId equals color.Id
                             join carImage in context.CarImages on car.Id equals carImage.CarId into images
                             from img in images.DefaultIfEmpty()
                             group new { car, brand, color, img } by car.Id into grouped
                             select new SoftCarDto
                             {
                                 Id = grouped.Key,
                                 BrandName = grouped.First().brand.BrandName,
                                 Description = grouped.First().car.Description,
                                 DailyPrice = grouped.First().car.DailyPrice,
                                 ModelYear = grouped.First().car.ModelYear,
                                 ColorName = grouped.First().color.ColorName,
                                 CoverPath = grouped.FirstOrDefault().img != null && !string.IsNullOrEmpty(grouped.FirstOrDefault().img.ImagePath)
                                             ? Path.GetFileName(grouped.FirstOrDefault().img.ImagePath) 
                                             : "5c9e290c-1079-4bcf-8913-a97da764e092.jpg"
                             };

                return result.ToList();


            }
        }
        public List<ImageCarDto> GetCarDto()
        {
            using (RentCarContext context = new RentCarContext())
            {

                var result = from car in context.Cars
                             join brand in context.Brands on car.BrandId equals brand.Id
                             join color in context.Colors on car.ColorId equals color.Id
                             join carImage in context.CarImages on car.Id equals carImage.CarId into images
                             from img in images.DefaultIfEmpty() 
                             group new { car, brand, color, img } by car.Id into grouped
                             select new ImageCarDto
                             {
                                 Id = grouped.Key,
                                 BrandName = grouped.First().brand.BrandName,
                                 Description = grouped.First().car.Description,
                                 DailyPrice = grouped.First().car.DailyPrice,
                                 ModelYear = grouped.First().car.ModelYear,
                                 ColorName = grouped.First().color.ColorName,
                                 ImagePath = grouped
    .Where(x => x.img != null && !string.IsNullOrEmpty(x.img.ImagePath))
    .Select(x => System.IO.Path.GetFileName(x.img.ImagePath))
    .DefaultIfEmpty("b3e07f6e-9be9-48a0-bcaa-fcc0a0ff53f9.jpg") // Resim yoksa varsayılanı ekle
    .ToList()


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

        public List<ImageCarDto> GetCarDetailByCarId(int carId)
        {
            using (var context = new RentCarContext())
            {
                var flat = (from c in context.Cars
                            join b in context.Brands on c.BrandId equals b.Id
                            join co in context.Colors on c.ColorId equals co.Id
                            join ci in context.CarImages on c.Id equals ci.CarId into carImages
                            from image in carImages.DefaultIfEmpty()
                            where c.Id == carId
                            select new
                            {
                                c.Id,
                                c.BrandId,
                                BrandName = b.BrandName,
                                ColorName = co.ColorName,
                                c.DailyPrice,
                                c.Description,
                                c.ModelYear,
                                ImagePath = image != null
                                              ? Path.GetFileName(image.ImagePath)
                                              : "5c9e290c-1079-4bcf-8913-a97da764e092.jpg"
                            })
            .AsEnumerable();    // ← buradan sonrası LINQ-to-Objects

                // 2) Bellekte grupla, DTO’ya dönüştür
                var result = flat
                    .GroupBy(x => x.Id)
                    .Select(g => new ImageCarDto
                    {
                        Id = g.Key,
                        BrandName = g.First().BrandName,
                        Description = g.First().Description,
                        DailyPrice = g.First().DailyPrice,
                        ModelYear = g.First().ModelYear,
                        ColorName = g.First().ColorName,
                        ImagePath = g
                            .Select(x => x.ImagePath)
                            .Distinct()
                            .ToList()
                    })
                    .ToList();

                return result;
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
