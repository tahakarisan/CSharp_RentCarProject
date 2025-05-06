using Business.Abstract;
using CoreLayer.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Favs : Controller
    {
        IFavCarService _favCarService;
        public Favs(IFavCarService favCarService)
        {
            _favCarService = favCarService;
        }
        [HttpPost("addFavCar")]
        public IActionResult AddFavCar(FavCar favCar)
        {
            var result = _favCarService.Add(favCar);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpDelete("deleteFavCar")]
        public IActionResult DeleteFavCar(int favCarId, int userId)
        {
            var result = _favCarService.Delete(favCarId,userId);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPut("updateFavCar")]
        public IActionResult UpdateFavCar(FavCar favCar)
        {
            var result = _favCarService.Update(favCar);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("getFavCars")]
        public IActionResult GetFavCarsByUserId(int userId)
        {
            var result = _favCarService.GetFavCarsByUserId(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
