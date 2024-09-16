using CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class CarDto : IDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string BrandId { get; set; }
        public decimal DailyPrice { get; set; }
    }
}

