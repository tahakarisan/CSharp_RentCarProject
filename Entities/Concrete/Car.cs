﻿using CoreLayer.Entities.Concrete;

namespace Entities.Concrete
{
    public class Car : BaseEntity
    {
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public int ModelYear { get; set; }
        public int DailyPrice { get; set; }
        public string Description { get; set; }
    }
}
