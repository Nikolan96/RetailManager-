using System;
using System.Collections.Generic;
using System.Text;

namespace RMDataManagerCore.Library.Models
{
    public class OrderModel
    {
        public string ID { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ShopID { get; set; }

        public bool IsApproved { get; set; }
    }
}
