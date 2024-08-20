using Business.Abstract;
using Business.Constant;
using CoreLayer.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {

        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }


        public IResult Add(User user)
        {
            if (user.FirstName != null || user.Email != null || user.Password != null)
            {
                return new SuccesfullResult(Messages.ErrorUserAdded);
            }
            else
            {
                return new ErrorResult(Messages.OperationsSuccesful);
            }
        }

        public IResult Delete(int id)
        {
            return new SuccesfullResult(Messages.UserDeleted);
        }

        public IDataResult<List<User>> GetAll()
        {
            return new DataSuccesfullResult<List<User>>(_userDal.GetAll(), Messages.UserListed);
        }

        public IDataResult<List<User>> GetUserById(int id)
        {

            return new DataSuccesfullResult<List<User>>(_userDal.GetAll(u=>u.Id==id),Messages.UserGetById);
        }

        public IResult Update(User user)
        {
            return new SuccesfullResult(Messages.UserUpdated);
        }

   
    }
}
