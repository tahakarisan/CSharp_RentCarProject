using CoreLayer.Entities.Concrete;
using CoreLayer.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRoleService
    {
        IResult AddRole(UserOperationClaim userOperationClaim);
        IDataResult<List<UserOperationClaim>> GetUserRoles(UserOperationClaim userOperationClaim);
        IResult DeleteRole(UserOperationClaim userOperationClaim);
    }
}
