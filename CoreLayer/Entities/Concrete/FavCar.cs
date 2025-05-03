using CoreLayer.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities.Concrete
{
    public class FavCar:BaseEntity
    {
        public int UserId { get; set; }
        public int CarId { get; set; }
        public int IsRental { get; set; }
    }
}
