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
        ICarService _carService;
        IRentalDal _rentalDal;
        public RentalManager(IRentalDal rentalDal, ICarService carService)
        {
            _rentalDal = rentalDal;
            _carService = carService;
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
            if (IsTheCarOverloaded(rentalInfo).Success)
            {
                _rentalDal.Add(rentalInfo);
                return new SuccesfullResult("Araba başarıyla kiralandı");
            }

            return new ErrorResult("Araba kiralanamaz çünkü şirket politikamıza göre araçlarımız ayda maksimum 10 kere kiralanabilir");

        }
        [ValidationAspect(typeof(UpdateRentalValidator))]
        public IResult Update(RentalInfo rentalInfo)
        {
            _rentalDal.Update(rentalInfo);
            return new SuccesfullResult("Veri güncellendi");
        }
        public IResult IsTheCarOverloaded(RentalInfo rentalInfo)
        {
           
            var result = _rentalDal.GetAll(r => r.CarId == rentalInfo.CarId && r.RentDate>rentalInfo.RentDate.AddMonths(-1));
            if (result.Count > 10)
            {
                return new ErrorResult();
            }
            return new SuccesfullResult();
        }
    }
}
