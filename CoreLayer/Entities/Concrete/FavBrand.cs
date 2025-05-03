using CoreLayer.Entities.Abstract;

namespace CoreLayer.Entities.Concrete
{
    public class FavBrand:BaseEntity
    {
        public int UserId { get; set; }
        public int BrandId { get; set; }
    }
}
