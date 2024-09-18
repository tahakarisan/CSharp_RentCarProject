using CoreLayer.Entities.Concrete;
using CoreLayer.Utilities.Results;
using CoreLayer.Utilities.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        IResult Add(User user);
        User GetByMail(string email);
        IDataResult<List<User>> GetAll();
        IResult Delete(int id);
        IResult Update(User user);
        IDataResult<List<OperationClaim>> GetClaims(User user);
    }
}
