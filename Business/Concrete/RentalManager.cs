using Business.Abstract;
using Business.Constant;
using Business.ValidationRules.AddValidation;
using Business.ValidationRules.UpdateValidation;
using CoreLayer.Aspects.Autofac;
using CoreLayer.Utilities.Business;
using CoreLayer.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var result = _rentalDal.FirstOrDefault(r=>r.Id==id);
            if (result!=null)
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
                return new ErrorDataResult<List<RentalInfo>>(Messages.ListInMaintenance);
            }

            return new SuccesfulDataResult<List<RentalInfo>>(_rentalDal.GetAll(), "Kiralamalar listelendi");
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(RentalInfo rentalInfo)
        {
            IResult result = BusinessRules.Run(IsTheCarOverloaded(rentalInfo),IsItForRent(rentalInfo),DailyRentalLimit(rentalInfo));
            if (result == null)
            {
                _rentalDal.Add(rentalInfo);
                return new SuccesfullResult("Kiralama işlemi başarılı");
            }
            if (result.Success)
            {
                if (FirstRent(rentalInfo).Success)
                {
                   _rentalDal.Add(rentalInfo);
                    return new SuccesfullResult("Kiralama ücretine ilk müşteri olmanıza özel %40 indirim yapıldı ");
                }
                _rentalDal.Add(rentalInfo);
                return new SuccesfullResult("Kiralama işlemi başarılı");
            }

            return new ErrorResult(result.Message);

        }
        [ValidationAspect(typeof(UpdateRentalValidator))]
        public IResult Update(RentalInfo rentalInfo)
        {
            var result = _rentalDal.FirstOrDefault(r=>r.Id==rentalInfo.Id);
            if (result==null)
            {
                return new ErrorResult("Güncellenemedi");
            }
            _rentalDal.Update(rentalInfo);
            return new SuccesfullResult("Veri güncellendi");
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------
        private IResult IsTheCarOverloaded(RentalInfo rentalInfo)
        {
           
            var result = _rentalDal.GetAll(r => r.CarId == rentalInfo.CarId && r.RentDate>rentalInfo.RentDate.AddMonths(-1));
            if (result.Count > 10)
            {
                return new ErrorResult("Şirket politikamıza göre bir araç ayda maksimum 10 kere kiralanabilir");
            }
            return new SuccesfullResult();
        }
        private IResult IsItForRent(RentalInfo rentalInfo)
        {
            var result = _rentalDal.GetAll(r=>r.ReturnDate>rentalInfo.RentDate);
            if (result.Any())
            {
                return new ErrorResult("Araba kiralık durumda. Şirkete hala teslim edilmedi");
            }
            return new SuccesfullResult();
        }
        private IResult DailyRentalLimit(RentalInfo rentalInfo)
        {
            var result = _rentalDal.GetAll(r=>r.CustomerId==rentalInfo.CustomerId).FirstOrDefault();
            bool checkRentDate = result.RentDate.AddDays(+1) >= rentalInfo.RentDate;
            if(checkRentDate)
            {
                return new ErrorResult("Günlük araba kiralama hakkınız bitmiştir");
            }
            return new SuccesfullResult();
        }
        private IResult FirstRent(RentalInfo rentalInfo)
        {
            var result = _rentalDal.GetAll(r => r.CustomerId == rentalInfo.CustomerId).FirstOrDefault();

            if (result == null)
            {
                var car = _carService.GetById(rentalInfo.CarId);
                if (car == null)
                {
                    return new ErrorResult("Araba bulunamadı.");  // Eğer araç bulunamazsa hata döndür
                }
                var salePrice = car.Data.DailyPrice * 0.6m;
                return new SuccesfullResult();
            }    
            return new ErrorResult();
        }
    }
}
