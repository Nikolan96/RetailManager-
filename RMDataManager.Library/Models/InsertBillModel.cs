using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.Models
{
    public class InsertBillModel
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ShopId { get; set; }
        public decimal Total { get; set; }
        public decimal Paid { get; set; }
        public decimal Change { get; set; }
        public string UserId { get; set; }
    }
}
