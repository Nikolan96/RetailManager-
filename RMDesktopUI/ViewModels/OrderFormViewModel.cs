using Caliburn.Micro;
using RMDesktopUI.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
    public class OrderFormViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;

        public OrderFormViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void GoToProductsView()
        {
            _eventAggregator.PublishOnUIThread(new ProductsViewEvent());
        }

        public void Order()
        {
            // add Order to db, Add orderItems to db, clear datagrid
            _eventAggregator.PublishOnUIThread(new ProductsViewEvent());
        }
    }
}
