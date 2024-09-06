using CoreLayer.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFRentalInfoDal : EFEntityRepositoryBase<RentalInfo, RentCarContext>, IRentalDal
    {

        /// <summary>
        /// CarId'ye göre aracın müsaitlik durumunu belirtir.
        /// Araç kiralanmaya uygunsa true,değilse false döndürür.
        /// </summary>
        /// <param name="carId"></param>
        /// <returns>bool</returns>
    }
}