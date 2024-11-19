using CoreLayer.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface ICarImageDal: IEntityRepository<CarImage>
    {
        List<CarImage> GetCarImage(Expression<Func<CarImage, bool>> filter = null);
    }
}
