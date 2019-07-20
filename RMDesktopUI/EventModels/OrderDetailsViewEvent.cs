using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.EventModels
{
    public class OrderDetailsViewEvent : INavigationEvent
    {
        public string OrderID { get; set; }
        public bool IsApproved { get; set; }

        public OrderDetailsViewEvent(string OrderID, bool IsApproved)
        {
            this.OrderID = OrderID;
            this.IsApproved = IsApproved;
        }
    }
}
