using CoreLayer.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFCarImageDal : EFEntityRepositoryBase<CarImage, RentCarContext>, ICarImageDal
    {
    }
}
