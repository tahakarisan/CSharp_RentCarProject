using CoreLayer.DataAccess;
using CoreLayer.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFUserDal : EFEntityRepositoryBase<User, RentCarContext>, IUserDal
    {

        public List<OperationClaim> GetClaims(User user)
        {
            using (RentCarContext context = new RentCarContext())
            {
                var query = from operationClaims in context.OperationClaims
                            join userOperationClaims in context.UserOperationClaims
                            on operationClaims.Id equals userOperationClaims.OperationClaimId
                            where user.Id == userOperationClaims.UserId
                            select new OperationClaim
                            {
                                Id = operationClaims.Id,
                                Name = operationClaims.Name,
                            };
                return query.ToList();

            }
        }

        public UserDto? GetUserById(int userId)
        {
            using (var context = new RentCarContext())
            {
                var userDto = context.Users
                    .Where(u => u.Id == userId)
                    .Select(u => new UserDto
                    {
                        Id = u.Id,
                        Email = u.Email,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Status = u.Status
                    })
                    .FirstOrDefault();

                return userDto;
            }
        }

        public bool DefineCampaign(UserCampaign userCampaign)
        {
            using (var context = new RentCarContext())
            {
                var define = context.Entry(userCampaign);
                define.State = EntityState.Added;
              
                return  context.SaveChanges()>0;
           
            }
        }
    }
}
