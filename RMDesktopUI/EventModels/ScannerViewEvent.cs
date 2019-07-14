using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.EventModels
{
    public class ScannerViewEvent
    {
        public ScannerViewEvent(INavigationEventWithParameters navigateTo)
        {
            NavigateTo = navigateTo;
        }

        public INavigationEventWithParameters NavigateTo { get; }
    }
}
