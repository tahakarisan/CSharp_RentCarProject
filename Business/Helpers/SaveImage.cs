using CoreLayer.Utilities.Results;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Helpers
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SaveImageAttribute : Attribute
    {
        public IResult Save(UploadImageDTO fileModel)
        {
            if (fileModel != null)
            {
                var extent = Path.GetExtension(fileModel.RequestedFormFile.FileName);
                var randomName = $"{Guid.NewGuid()}{extent}";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", randomName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    fileModel.RequestedFormFile.CopyTo(stream);
                }

                return new SuccesfullResult();
            }

            return null;
        }
    }
}
