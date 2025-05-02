using CoreLayer.Entities.Concrete;

namespace Entities.Concrete
{
    public class UserTokenData : BaseEntity
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
