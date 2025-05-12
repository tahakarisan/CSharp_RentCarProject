using CoreLayer.DataAccess;
using CoreLayer.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFRoleDal : EFEntityRepositoryBase<UserOperationClaim, RentCarContext>,IRoleDal
    {
        public List<OperationClaim> GetRoles()
        {
            using (RentCarContext context = new RentCarContext())
            {
                return context.Set<OperationClaim>().ToList();
            }
        }

        public List<UserRoleDto> GetUserRoleDtos()
        {
            using (RentCarContext context = new RentCarContext())
            {
                var query = from user in context.Users
                            join userOperationClaim in context.UserOperationClaims
                            on user.Id equals userOperationClaim.UserId
                            join operationClaim in context.OperationClaims
                            on userOperationClaim.OperationClaimId equals
                            operationClaim.Id
                            select new UserRoleDto()
                            {
                                Id = userOperationClaim.Id,
                                Email = user.Email,
                                RoleName = operationClaim.Name,
                                UserName = user.FirstName,
                                UserLastName = user.LastName,

                            };
                return query.ToList();           

            }   
        }
    }
}
