using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.EventModels
{
    public class BarcodeScannedEvent : INavigationEvent
    {
        public string ProductID { get; set; }

        public BarcodeScannedEvent(string ProductID)
        {
            this.ProductID = ProductID;
        }
    }
}
