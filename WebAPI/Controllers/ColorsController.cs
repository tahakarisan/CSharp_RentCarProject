using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {

        IColorService _colorService;
        public ColorsController(IColorService colorService)
        {
            _colorService = colorService;
        }
        [HttpGet("GetAllColor")]
        public IActionResult GetAll()
        {
            var result = _colorService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("AddColor")]
        public IActionResult Add(Color color) 
        {
            var result = _colorService.Add(color);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        
        }
        [HttpDelete("DeleteColor")]
        public IActionResult Delete(int id) 
        {
            var result = _colorService.Delete(id);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            
            return BadRequest(result.Message);
        }
        [HttpPut("UpdateColor")]
        public IActionResult Update(Color color)
        {
            var result = _colorService.Update(color);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            
            return BadRequest(result.Message);
        }
    }
}
