using Microsoft.AspNetCore.Http;

namespace Entities.Dtos
{
    public class UploadImageDTO
    {
        public int CarId { get; set; }
        public string Name { get; set; }
        public IFormFile RequestedFormFile { get; set; }
    }
}

