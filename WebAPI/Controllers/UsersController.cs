﻿using Business.Abstract;
using CoreLayer.Entities.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _userService;
        
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("GetAllUser")]
        public IActionResult Get()
        {
            var result = _userService.GetAll();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getInfoUser")]
        public IActionResult GetUserByToken(string token)
        {
            var result = _userService.GetUserByToken(token);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpGet("getUserById")]
        public IActionResult GetUserById(int id)
        {
            var result = _userService.GetUserById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost("AddUser")]
        public IActionResult Add(User user)
        {
            var result = _userService.Add(user);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        [HttpPut("UpdateUser")]
        public IActionResult Update(User user)
        {
            var result = _userService.Update(user);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        [HttpDelete("DeleteUser")]
        public IActionResult Delete(int id)
        {
            var result = _userService.Delete(id);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("AddCampaign")]
        public IActionResult AddCampaign(UserCampaign userCampaign)
        {
            var result = _userService.CampaignDefine(userCampaign);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }


    }
}
