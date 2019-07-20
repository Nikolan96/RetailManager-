using System;
using System.Collections.Generic;
using System.Text;

namespace RMDesktopUI.Library.Models
{
    public class OrderItemModel
    {
        public int ID { get; set; }

        public string OrderID { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public string ProductID { get; set; }

    }
}
