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
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;
        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }
        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer customer)
        {
            _customerDal.Add(customer);
            return new SuccesfullResult("Kullanıcı Eklendi");
        }

        public IResult Delete(int id)
        {
            if (!_customerDal.GetAll(c => c.Id == id).Any())
            {
                return new ErrorResult("Zaten böyle bir veri bulunmuyor");
            }
            _customerDal.Delete(id);
            return new SuccesfullResult("Müşteri silindi");
        }

        [ValidationAspect(typeof(UpdateCustomerValidator))]
        public IResult Update(Customer customer)
        {
            _customerDal.Update(customer);
            return new SuccesfullResult("Müşteri başarıyla güncellendi");
        }
        [CacheAspect]
        public IDataResult<List<Customer>> GetAll()
        {
            if (DateTime.Now.Hour == 16 || DateTime.Now.Hour == 8)
            {
                return new ErrorDataResult<List<Customer>>(Messages.ListInMaintenance);
            }
            return new SuccesfulDataResult<List<Customer>>(_customerDal.GetAll(), Messages.CarListed);
        }

    }
}
