using Business.Abstract;
using Business.Constant;
using Business.ValidationRules.AddValidation;
using Business.ValidationRules.UpdateValidation;
using CoreLayer.Aspects.Autofac;
using CoreLayer.Utilities.Results;
using DataAccess.Abstract;
using Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            if (!_rentalDal.GetAll(r => r.Id == id).Any())
            {
                return new ErrorResult("Zaten böyle bir veri mevcut değil");
            }
            _rentalDal.Delete(id);
            return new SuccesfullResult("Araba kiralama verisi silindi");
        }

        public IDataResult<List<RentalInfo>> GetAll()
        {
            if (DateTime.Now.Hour == 4)
            {
                return new DataErrorResult<List<RentalInfo>>(Messages.ListInMaintenance);
            }

            return new DataSuccesfullResult<List<RentalInfo>>(_rentalDal.GetAll(), "Kiralamalar listelendi");
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(RentalInfo rentalInfo)
        {
            _rentalDal.Add(rentalInfo);
            return new SuccesfullResult("Araba başarıyla kiralandı");

        }
        [ValidationAspect(typeof(UpdateRentalValidator))]
        public IResult Update(RentalInfo rentalInfo)
        {
            _rentalDal.Update(rentalInfo);
            return new SuccesfullResult("Veri güncellendi");
        }
    }
}
