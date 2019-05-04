using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace RMDesktopUI.ViewModels
{
    // Conductor holds on to and activates only one item at a time.
    public class ShellViewModel : Conductor<object>
    {
        private LoginViewModel _loginVM;

        // Uses constructor injection to pass in a new instance of LoginVM and activate it.
        public ShellViewModel(LoginViewModel loginVM)
        {
            _loginVM = loginVM;
            ActivateItem(_loginVM);
        }
    }
}
