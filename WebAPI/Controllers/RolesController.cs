using Business.Abstract;
using CoreLayer.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        IRoleService _roleService;
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet("UserRoleList")]
        public IActionResult GetUserRoles(UserOperationClaim userOperationClaim)
        {
            var result = _roleService.GetUserRoles(userOperationClaim);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("AddUserRole")]
        public IActionResult AddUser(UserOperationClaim userOperationClaim)
        {
            var result = _roleService.AddRole(userOperationClaim);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("DeleteUserRole")]
        public IActionResult DeleteUserRole(UserOperationClaim userOperationClaim)
        {
            var result = _roleService.DeleteRole(userOperationClaim);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
