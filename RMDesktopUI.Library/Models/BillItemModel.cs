using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.Library.Models
{
    public class BillItemModel
    {
        public int Id { get; set; }
        public string BillId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal RetailPrice { get; set; }
        public string ProductID { get; set; }

    }
}
