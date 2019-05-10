using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using RMDesktopUI.EventModels;

namespace RMDesktopUI.ViewModels
{
    // Conductor holds on to and activates only one item at a time.
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private readonly IEventAggregator _events;
        private readonly SalesViewModel _salesVM;
        private readonly SimpleContainer _container;

        // Uses constructor injection to pass in a new instance of LoginVM and activate it.
        public ShellViewModel(IEventAggregator events, SalesViewModel salesVM, SimpleContainer container)
        {
            _events = events;
            _salesVM = salesVM;
            _container = container;

            // Subscribes instance of shellview to events
            _events.Subscribe(this);

            ActivateItem(_container.GetInstance<LoginViewModel>()); // gets new instance of LoginViewModel and places it in _loginVM ( cleanes info from last one ).
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesVM);
            

        }
    }
}
