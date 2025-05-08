using CoreLayer.Entities.Concrete;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
        UserDto GetUserById(int userId);
        bool DefineCampaign(UserCampaign userCampaign);
    }
}
