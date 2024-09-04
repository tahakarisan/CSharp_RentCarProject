using Entities.Abstract;

namespace DataAccess.Concrete.EntityFramework
{
    public class Image : IEntity
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public byte[]  CarImage { get; set; }
    }
}
