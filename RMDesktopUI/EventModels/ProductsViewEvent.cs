using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.EventModels
{
    public class ProductsViewEvent : INavigationEvent
    {
    }

    public class ProductsViewEventWithScanResult : INavigationEventWithParameters
    {
        public object Parameters { get; set; }
    }
}
