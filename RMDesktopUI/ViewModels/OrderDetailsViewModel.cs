using Caliburn.Micro;
using RMDesktopUI.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
    public class OrderDetailsViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;

        public OrderDetailsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void EditOrderItem()
        {
            _eventAggregator.PublishOnUIThread(new EditOrderItemViewEvent());
        }

        public void Back()
        {
            _eventAggregator.PublishOnUIThread(new OrdersViewEvent());
        }
    }
}
