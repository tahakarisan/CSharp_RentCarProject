using CoreLayer.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IResult RentalAdd(RentalInfo rentalInfo);
        IResult Delete(int id);
        IResult Update(int id);
        IDataResult<RentalInfo> GetAll();

    }
}
