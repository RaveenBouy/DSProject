using System;
using System.Collections.Generic;
using System.Text;


namespace DataLibrary.Models
{
    public class AdvertisementModel
	{
		public int? ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int? ItemCount { get; set; }
        public string City { get; set; }
        public string ItemCategory { get; set; }
        public string Tele { get; set; }
        public decimal? Price { get; set; }
        //public blob Image { get; set; }
        public string Con{ get; set; }
		public string Negotiable { get; set; }
		public string Created { get; set; }
		public string Updated { get; set; }
		public int? ViewCount { get; set; }
	}
}
