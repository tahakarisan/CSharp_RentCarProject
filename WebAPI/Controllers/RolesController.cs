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
        //[HttpGet("UserRoleList")]
        //public IActionResult GetUserRoles()
        /*{
            var result = _roleService.g();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }*/
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

        [HttpGet("getUserRoleDto")]
        public IActionResult GetUserRoleDtos()
        {
            var result = _roleService.GetUserRoleDtos();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getRoles")]
        public IActionResult GetRoles()
        {
            var result = _roleService.GetAllRole();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

    }
}
