using CoreLayer.DataAccess;
using CoreLayer.Entities;
using CoreLayer.Entities.Concrete;
using CoreLayer.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFCarImageDal : EFEntityRepositoryBase<CarImage, RentCarContext>, ICarImageDal
    {
        public List<CarImage> GetCarImage(Expression<Func<CarImage, bool>> filter = null)
        {
            using (var context = new RentCarContext())
            {
                var result = context.CarImages.Select(carImage => new CarImage
                {
                    Id = carImage.Id,
                    CarId = carImage.CarId,
                    ImagePath = carImage.ImagePath.Remove(0,"C:\\Users\\SoftwareHP\\Desktop\\ReCap\\WebAPI\\wwwroot\\images\\".Length),
                    Name = carImage.Name
                });

                return result.ToList();
                           
             }
        }
    }
}