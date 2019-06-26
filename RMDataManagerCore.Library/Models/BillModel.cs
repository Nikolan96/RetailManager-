using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManagerCore.Library.Models
{
    public class BillModel
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ShopId { get; set; }
        public decimal Total { get; set; }
        public decimal Paid { get; set; }
        public decimal Change { get; set; }
        public string UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
