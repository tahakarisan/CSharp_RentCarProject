using CoreLayer.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Runtime.ConstrainedExecution;

namespace Business.Helpers.FileHelper
{
    public class FileHelper
    {
        public static IDataResult<string> AddImage(IFormFile file)
        {
            if (file == null)
            {
                var defaultImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", "Karışan Rent A Car.jpg");
                return new DataErrorResult<string>(defaultImage);
            }
            var extent = Path.GetExtension(file.FileName);
            var randomName = $"{Guid.NewGuid()}{extent}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", randomName);
            try
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return new DataSuccesfullResult<string>(data: path, message: "Eklendi");
            }
            catch (Exception)
            {
                return new DataErrorResult<string>(data:null);
            }
        }
    }
}
