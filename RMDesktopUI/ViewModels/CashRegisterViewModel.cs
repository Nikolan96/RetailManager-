using Caliburn.Micro;
using RMDesktopUI.Library.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
    public class CashRegisterViewModel : Screen
    {
        private IAPIHelper _apiHelper;
        private IEventAggregator _events;

        public CashRegisterViewModel(IAPIHelper apiHelper, IEventAggregator events)
        {
            _apiHelper = apiHelper;
            _events = events;
        }
    }
}
