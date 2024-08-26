using CoreLayer.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IResult Add(Customer Customer);
        IDataResult<List<Customer>> GetAll();
        IResult Delete(int id);
        IResult Update(Customer customer);
    }
}
    