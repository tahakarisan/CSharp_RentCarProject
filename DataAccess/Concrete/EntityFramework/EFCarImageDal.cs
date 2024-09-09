using CoreLayer.DataAccess;
using CoreLayer.Entities;
using CoreLayer.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFCarImageDal : EFEntityRepositoryBase<CarImage, RentCarContext>, ICarImageDal
    {
    }
}
