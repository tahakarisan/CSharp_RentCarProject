using CoreLayer.Entities;

namespace Entities.Concrete
{
    public class CarImage : BaseEntity
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int CarId { get; set; }
    }
}
