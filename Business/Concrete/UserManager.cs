using Business.Abstract;
using Business.Constant;
using Business.ValidationRules.AddValidation;
using Business.ValidationRules.UpdateValidation;
using CoreLayer.Aspects.Autofac;
using CoreLayer.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccesfullResult("Kullanıcı eklendi");
         
        }

        public IResult Delete(int id)
        {
            _userDal.Delete(id);
            return new SuccesfullResult(Messages.UserDeleted);
        }

        public IDataResult<List<User>> GetAll()
        {
            if (DateTime.Now.Hour==4)
            {
                return new DataErrorResult<List<User>>(Messages.ListInMaintenance);
            }
            return new DataSuccesfullResult<List<User>>(_userDal.GetAll(), Messages.UserListed);
        }

        public IDataResult<List<User>> GetUserById(int id)
        {
            return new DataSuccesfullResult<List<User>>(_userDal.GetAll(u=>u.Id==id),Messages.UserGetById);
        }

        [ValidationAspect(typeof(UpdateUserValidator))]
        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccesfullResult(Messages.UserUpdated);
        }

   
    }
}
