using Business.Abstract;
using Business.BusinessAspect.Autofac;
using CoreLayer.Entities.Concrete;
using CoreLayer.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RoleManager : IRoleService
    {
        IUserService _userService;
        IRoleDal _roleDal;
        public RoleManager(IUserService userService, IRoleDal roleDal)
        {
            _userService = userService;
            _roleDal = roleDal;
        }
        [SecuredOperation("ceo")]
        public IResult AddRole(UserOperationClaim userOperationClaim)
        {
            var findClaims = _roleDal.FirstOrDefault(u => u.OperationClaimId == userOperationClaim.OperationClaimId || u.UserId != userOperationClaim.UserId);
            if (findClaims == null)
            {
                var userClaims = new UserOperationClaim
                {
                    UserId = userOperationClaim.UserId,
                    OperationClaimId = userOperationClaim.OperationClaimId,
                };
                _roleDal.Add(userOperationClaim);
                return new SuccesfullResult("Rol eklendi");
            }
            return new ErrorResult("Rol eklenemedi");
        }

        public IResult DeleteRole(UserOperationClaim userOperationClaim)
        {
            var findClaims = _roleDal.FirstOrDefault(u=>u.Id==userOperationClaim.Id);
            if (findClaims == null)
            {
                return new ErrorResult("Böyle bir rol bulunamadı");
            }
            _roleDal.Delete(userOperationClaim.Id);
            return new SuccesfullResult("Rol başarıyla silindi");
        }

        public IDataResult<List<UserOperationClaim>> GetUserRoles(UserOperationClaim userOperationClaim)
        {
            var result = _roleDal.GetAll(u=>u.UserId==userOperationClaim.UserId);
            if(result==null)
            {
                return new ErrorDataResult<List<UserOperationClaim>>("Girdiğiniz kullanıcın herhangi bir rolü bulunmuyor");
            }
            return new SuccesfulDataResult<List<UserOperationClaim>>(data: result);
            
        }
    }
}

