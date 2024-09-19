using Business.Abstract;
using Business.Constant;
using Business.ValidationRules.AddValidation;
using Business.ValidationRules.UpdateValidation;
using CoreLayer.Aspects.Autofac;
using CoreLayer.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;
        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        [ValidationAspect(typeof(BrandValidator))]
        public IResult Add(Brand brand)
        {
            _brandDal.Add(brand);
            return new SuccesfullResult("Marka başarıyla eklendi");
        }

        public IResult Delete(int id)
        {
            if (!_brandDal.GetAll(b=>b.Id==id).Any())
            {
                return new ErrorResult("Zaten böyle bir veri bulunmuyor");
            }
            _brandDal.Delete(id);
            return new SuccesfullResult("Marka başarıyla silindi");
        }

        public IDataResult<List<Brand>> GetAll()
        {
            if (DateTime.Now.Hour == 3 || DateTime.Now.Hour == 6)
            {
                return new ErrorDataResult<List<Brand>>(Messages.ListInMaintenance);
            }
            return new SuccesfulDataResult<List<Brand>>(_brandDal.GetAll(),"Markalar listelendi");
        }

        public IDataResult<List<Brand>> GetById(int id)
        {
            if (DateTime.Now.Hour == 3 || DateTime.Now.Hour == 6)
            {
                return new ErrorDataResult<List<Brand>>(Messages.ListInMaintenance);
            }
            return new SuccesfulDataResult<List<Brand>>(_brandDal.GetAll(b=>b.Id==id));
        }

        [ValidationAspect(typeof(UpdateBrandValidator))]
        public IResult Update(Brand brand)
        {
            _brandDal.Update(brand);
            return new SuccesfullResult("Marka başarıyla güncellendi");
        }
    }
}
