using CoreLayer.Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IRoleDal : IEntityRepository<UserOperationClaim>
    {
        List<OperationClaim> GetRoles();
        List<UserRoleDto> GetUserRoleDtos();
    }
}
