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
        public int BillId { get; set; }
        public int ProductName { get; set; }
        public int Category { get; set; }
        public int Description { get; set; }
        public int Quantity { get; set; }
        public int RetailPrice { get; set; }

    }
}
