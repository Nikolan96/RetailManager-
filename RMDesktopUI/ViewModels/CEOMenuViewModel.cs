using Caliburn.Micro;
using RMDesktopUI.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
    public class CEOMenuViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;

        public CEOMenuViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void GoToShopListView()
        {
            _eventAggregator.PublishOnUIThread(new ShopListViewEvent());
        }

        public void GoToUserListView()
        {
            _eventAggregator.PublishOnUIThread(new UserListViewEvent());
        }

        public void GoToChartMenuView()
        {
            _eventAggregator.PublishOnUIThread(new ChartMenuViewEvent());
        }
    }
}
