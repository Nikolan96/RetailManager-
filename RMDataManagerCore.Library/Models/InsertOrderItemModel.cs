using System;
using System.Collections.Generic;
using System.Text;

namespace RMDataManagerCore.Library.Models
{
    public class InsertOrderItemModel
    {
        public string OrderID { get; set; }

        public string ProductName { get; set; }
 
        public int Quantity { get; set; }

        public string ProductID { get; set; }
    }
}
