using Caliburn.Micro;
using RMDesktopUI.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
    public class ProfitByCategoryViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;

        public ProfitByCategoryViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Back()
        {
            _eventAggregator.PublishOnUIThread(new ChartMenuViewEvent());
        }
    }
}
