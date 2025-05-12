using CoreLayer.Entities.Concrete;
using CoreLayer.Utilities.Results;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRoleService
    {
        IDataResult<List<UserRoleDto>> GetUserRoleDtos();
        IDataResult<List<OperationClaim>> GetAllRole();
        IResult AddRole(UserOperationClaim userOperationClaim);
        IDataResult<List<UserOperationClaim>> GetUserRoles(UserOperationClaim userOperationClaim);
        IResult DeleteRole(UserOperationClaim userOperationClaim);
    }
}
