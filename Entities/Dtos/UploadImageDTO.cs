using CoreLayer.Entities.Abstract;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Entities.Dtos
{
    public class UploadImageDTO : IDto
    {
        public int CarId { get; set; }
        public string Name { get; set; }
        public List<IFormFile> RequestedFormFile { get; set; }
    }
}

