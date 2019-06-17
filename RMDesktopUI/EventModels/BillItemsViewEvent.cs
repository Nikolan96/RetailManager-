using RMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.EventModels
{
    public class BillItemsViewEvent
    {
        public string BillId { get; set; }

        public BillItemsViewEvent(string BillId)
        {
            this.BillId = BillId;
        }
    }
}
