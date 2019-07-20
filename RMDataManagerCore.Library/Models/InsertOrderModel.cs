using System;
using System.Collections.Generic;
using System.Text;

namespace RMDataManagerCore.Library.Models
{
    public class InsertOrderModel
    {
        public string ID { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ShopID { get; set; }
    }
}
