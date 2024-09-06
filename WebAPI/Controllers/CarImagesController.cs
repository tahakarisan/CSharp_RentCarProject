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
        [HttpPost]
        public IActionResult UploadImage([FromForm] UploadImageDTO fileModel)//Post'un default'u FromBody, Get'in FromQuery,
        {
            if (fileModel != null)
            {
                try
                {
                    var extent = Path.GetExtension(fileModel.RequestedFormFile.FileName); //dosya uzantısını aldım file nameine göre
                    var randomName = ($"{Guid.NewGuid()}{extent}");
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", randomName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        fileModel.RequestedFormFile.CopyTo(stream);
                    }
                    CarImage carImage = new CarImage
                    {
                        CarId = fileModel.CarId,
                        ImagePath = path
                    };
                    _carImageService.UploadImage(carImage);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return Ok();
        }


        //Endpoint

        /*
         * Bussiness'da yapılacak.
        1-IFormFile'dan gelen dosyayı WebAPI'nin içerisindeki wwwroot/Images dosyasının içerisine kayıt eden bir Helper sınıfı yazılacak.
        2-UploadImage methodunda önce bu yazılan helper dosyayı kayıt edecek ve geriye dosya yolunu,işlem sonucunu dönecek.
        3-Eğer dosya kaydetme işlemi başarılı ise CarImages tablosuna CarId,ImagePath diye eklenicek.         
         */

    }
}
