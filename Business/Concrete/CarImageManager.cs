using Business.Abstract;
using CoreLayer.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageService _carImageService;
        public CarImageManager(ICarImageService carImageService)
        {
                _carImageService = carImageService;
        }
        public IResult UploadImage(IFormFile file)
        {
            if (file.Length==0 || file==null)
            {
                return new ErrorResult();
            }
            _carImageService.UploadImage(file);
            return new SuccesfullResult();
        }

        public IResult UpdateImage(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public IResult DeleteImage(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
