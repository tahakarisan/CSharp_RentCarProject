﻿using CoreLayer.Entities.Concrete;
using CoreLayer.Utilities.Results;
using CoreLayer.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.Dtos;
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
        IDataResult<UserDto> GetUserById(int userId);
        IDataResult<CoreLayer.Entities.Concrete.UserTokenData> GetUserByToken(string token);
        IResult Add(User user);
        User GetByMail(string email);
        IResult CampaignDefine(UserCampaign userCampaign);
        IDataResult<List<User>> GetAll();
        IResult Delete(int id);
        IResult Update(User user);
        List<OperationClaim> GetClaims(User user);
    }
}
