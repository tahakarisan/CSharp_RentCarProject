using CoreLayer.DataAccess;
using CoreLayer.Utilities.Results;
using DataAccess.Abstract;
using Entities.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Security;

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