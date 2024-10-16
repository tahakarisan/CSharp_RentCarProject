using CoreLayer.Entities.Concrete;

namespace Entities.Concrete
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
    }
}
