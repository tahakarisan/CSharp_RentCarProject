using CoreLayer.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities.Concrete
{
    public class UserTokenData: BaseEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

    }
}
