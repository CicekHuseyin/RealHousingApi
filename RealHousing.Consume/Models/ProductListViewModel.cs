﻿namespace RealHousing.Consume.Models
{
    public class ProductListViewModel
    {
        public int ProductID { get; set; }
        public string ProductTitle { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductType { get; set; }
        public string ProductAdress { get; set; }
        public int BedRoomCount { get; set; }
        public int BathCount { get; set; }
        public int Square { get; set; }
        public string CoverImageUrl { get; set; }
        public int CategoryID { get; set; }

    }
}
