using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        ICarImageService _carImageService;
        public CarImagesController(ICarImageService carImageService)
        {
            _carImageService = carImageService;
        }

        //Endpoint
        [HttpPost("UploadImage")]
        public IActionResult UploadImage([FromForm] UploadImageDTO fileModel)//Post'un default'u FromBody, Get'in FromQuery,
        {
            var result = _carImageService.UploadImage(fileModel);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("GetAllImage")]
        public IActionResult GetAll()
        {
            var result = _carImageService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetByCarId")]
        public IActionResult GetByCarId(int carId)
        {
            var result = _carImageService.GetImagesByCarId(carId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result); 
        }
        [HttpDelete("DeleteImage")]
        public IActionResult Delete(int id)
        {
            var result = _carImageService.DeleteImage(id);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        [HttpPut("UpdateImage")]
        public IActionResult Update([FromForm]UploadImageDTO fileModel)
        {
            var result = _carImageService.UpdateImage(fileModel);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }




        //Endpoint

        /*
         * Bussiness'da yapılacak.
        1-IFormFile'dan gelen dosyayı WebAPI'nin içerisindeki wwwroot/Images dosyasının içerisine kayıt eden bir Helper sınıfı yazılacak.+
        2-UploadImage methodunda önce bu yazılan helper dosyayı kayıt edecek ve geriye dosya yolunu,işlem sonucunu dönecek.
        3-Eğer dosya kaydetme işlemi başarılı ise CarImages tablosuna CarId,ImagePath diye eklenicek.         
         */

    }
}
