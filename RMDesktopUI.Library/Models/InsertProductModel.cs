using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.Library.Models
{
    public class InsertProductModel
    {
        public string ProductName { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal Tax { get; set; }

        public decimal RetailPrice { get; set; }

        public int Quantity { get; set; }

        public int ShopID { get; set; }
    }
}
