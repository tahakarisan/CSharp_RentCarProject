using Business.Abstract;
using Business.Constant;
using CoreLayer.Utilities.Results;
using DataAccess.Abstract;
using Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<RentalInfo> GetAll()
        {
            throw new NotImplementedException();
        }

        public IResult RentalAdd(RentalInfo rentalInfo)
        {
            bool isCarAvailable = (_rentalDal.GetAll(r => r.CarId == rentalInfo.CarId && r.ReturnDate > rentalInfo.RentDate).Any());

            if (isCarAvailable==false)
            {
                return new SuccesfullResult("Eklendi");
                
            }
            _rentalDal.Add(rentalInfo);
           
            return new ErrorResult("Eklenemez");
           
        }

        public IResult Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
