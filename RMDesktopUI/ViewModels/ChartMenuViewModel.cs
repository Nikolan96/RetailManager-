using Caliburn.Micro;
using RMDesktopUI.EventModels;
using RMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
    public class ChartMenuViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoggedInUserModel _loggedInUserModel;

        public ChartMenuViewModel(IEventAggregator eventAggregator, ILoggedInUserModel loggedInUserModel)
        {
            _eventAggregator = eventAggregator;
            _loggedInUserModel = loggedInUserModel;
        }

        public void Back()
        {
            if (_loggedInUserModel.Role == "CEO")
            {
                _eventAggregator.PublishOnUIThread(new CEOLogOnEvent());
            }
            else
            {
                _eventAggregator.PublishOnUIThread(new ManagerLogOnEvent());
            }
        }
    }
}
