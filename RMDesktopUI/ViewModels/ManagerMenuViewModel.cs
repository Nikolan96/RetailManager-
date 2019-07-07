using Caliburn.Micro;
using RMDesktopUI.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
    public class ManagerMenuViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;

        public ManagerMenuViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void GoToCashRegister()
        {
            _eventAggregator.PublishOnUIThread(new CashRegisterEvent());
        }

        public void GoToChartMenu()
        {
            _eventAggregator.PublishOnUIThread(new ChartMenuViewEvent());
        }
    }
}
