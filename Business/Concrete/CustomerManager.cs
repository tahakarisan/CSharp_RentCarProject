using Business.Abstract;
using Business.Constant;
using CoreLayer.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
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
        public IResult Add(Customer customer)
        {
            if (customer.CompanyName != null)
            {
                return new ErrorResult(Messages.ErrorUserAdded);
            }
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


        public IResult Update(Customer customer)
        {
            if (!_customerDal.GetAll(c => c.Id == customer.Id).Any())
            {
                return new ErrorResult("Güncellemek istediğiniz müşteri bulunamadı!");
            }
            _customerDal.Update(customer);
            return new SuccesfullResult("Müşteri başarıyla güncellendi");
        }

        public IDataResult<List<Customer>> GetAll()
        {
            if (DateTime.Now.Hour == 16 || DateTime.Now.Hour == 8)
            {
                return new DataErrorResult<List<Customer>>(Messages.ListInMaintenance);
            }
            return new DataSuccesfullResult<List<Customer>>(_customerDal.GetAll(), Messages.CarListed);
        }

    }
}
