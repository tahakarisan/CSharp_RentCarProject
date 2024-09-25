using Business.Abstract;
using Business.Constant;
using Business.ValidationRules.AddValidation;
using Business.ValidationRules.UpdateValidation;
using CoreLayer.Aspects.Autofac;
using CoreLayer.Entities.Concrete;
using CoreLayer.Utilities.Business;
using CoreLayer.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {

        IUserDal _userDal;
        ICampaignService _campaignService;
        ICampaignDal _campaignDal;
        public UserManager(IUserDal userDal,ICampaignService campaignService,ICampaignDal campaignDal)
        {
            _userDal = userDal;
            _campaignService = campaignService;
            _campaignDal = campaignDal;
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            IResult result = BusinessRules.Run(IsExistEmail(user));
            if (result == null)
            {
                _userDal.Add(user);
                return new SuccesfullResult("Kullanıcı eklendi");
            }
            if (result == null || result.Success)
            {
                _userDal.Add(user);
                return new SuccesfullResult("Kullanıcı eklendi");
            }
            return new ErrorResult(result.Message);
        }

        public IResult CampaignDefine(UserCampaign userCampaign)
        {
            var query = _userDal.FirstOrDefault(u=>u.Id==userCampaign.UserId);
            if (query==null)
            {
                return new ErrorResult("Kullanıcı Bulunamadı");
            }
            var result = _campaignService.GetUsersCampaigns(query);
            if (result != null)
            {
                return new ErrorResult("Bu kullanıcıya ait kampanya zaten var");
            }
            var campaignDefine = _campaignDal.AddUserCampaign(userCampaign);
            return new SuccesfullResult("Kampanya kullanıcıya tanımlandı");
        }

        public IResult Delete(int id)
        {
            _userDal.Delete(id);
            return new SuccesfullResult(Messages.UserDeleted);
        }
        public IDataResult<List<User>> GetAll()
        {
            if (DateTime.Now.Hour == 4)
            {
                return new ErrorDataResult<List<User>>(Messages.ListInMaintenance);
            }
            return new SuccesfulDataResult<List<User>>(_userDal.GetAll(), Messages.UserListed);
        }

        public User GetByMail(string email)
        {
            var result = _userDal.FirstOrDefault(u => u.Email == email);
            return result;
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            var claims = _userDal.GetClaims(user);
            if (claims != null && claims.Any())
            {
                return new SuccesfulDataResult<List<OperationClaim>>(claims);
            }
            return new ErrorDataResult<List<OperationClaim>>("");
        }
        public IDataResult<List<User>> GetUserById(int id)
        {
            return new SuccesfulDataResult<List<User>>(_userDal.GetAll(u => u.Id == id), Messages.UserGetById);
        }

        [ValidationAspect(typeof(UpdateUserValidator))]
        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccesfullResult(Messages.UserUpdated);
        }
        
        private IResult IsExistEmail(User user)
        {
            var result = _userDal.GetAll(u => u.Email == user.Email).Any();
            if (result)
            {
                return new ErrorResult("Bu e-mail siteye kayıtlı");
            }
            return new SuccesfullResult();
        }

        //private IResult InvalidPassword(User user)
        //{
        //    var result = user.Password.Contains(user.FirstName.ToLower());
        //    if (result)
        //    {
        //        return new ErrorResult("Şifrende isminin tamamını içermesi güvenlik problemlerine yol açabilir");
        //    }
        //    return new SuccesfullResult();
        //}


    }
}
