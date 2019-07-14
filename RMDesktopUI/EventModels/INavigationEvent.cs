using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.EventModels
{
    public interface INavigationEvent
    {
    }

    public interface INavigationEventWithParameters : INavigationEvent
    {
        object Parameters { get; set; }
    }

}
