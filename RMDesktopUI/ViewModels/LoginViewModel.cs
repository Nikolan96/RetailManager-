using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
    // Caliburn Micro wires up UI and logic behind the scene, based on names.
    public class LoginViewModel : Screen
    {
        private string _userName;
        private string _password;

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                // Indicates if UserName property changed value.
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                // Indicates if Password property changed value.
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        // Login logic that returns a boolean which is used to enable or disable login button.
        public bool CanLogIn
        {
            get
            {
                bool output = false;

                // ? after property name is a null check.
                if (UserName?.Length > 0 && Password?.Length > 0)
                {
                    output = true;
                }

                return output;
            }
        }

        // 
        public void LogIn()
        {
            Console.WriteLine();
        }




    }
}
