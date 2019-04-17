using System;
using System.Collections.Generic;
using System.Text;


namespace WebSite.Models
{
    public class AdvertisementModel
	{
		public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string ItemCount { get; set; }
        public string City { get; set; }
        public string ItemCategory { get; set; }
        public string Tele { get; set; }
        public string Price { get; set; }
        //public blob Image { get; set; }
        public string Con{ get; set; }
		public string Negotiable { get; set; }
		public string Created { get; set; }
		public string Updated { get; set; }
		public string ViewCount { get; set; }
	}
}
