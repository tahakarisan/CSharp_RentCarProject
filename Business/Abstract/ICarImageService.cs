using CoreLayer.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarImageService
    {
         IResult UploadImage(IFormFile file);
         IResult UpdateImage(IFormFile file);
         IResult DeleteImage(IFormFile file);
    }
}
