using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.Models
{
    public class InsertBillItemModel
    {
        public string BillId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal RetailPrice { get; set; }
    }
}
