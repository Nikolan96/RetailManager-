using Caliburn.Micro;
using RMDesktopUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMDesktopUI.Library.Api;
using RMDesktopUI.EventModels;
using System.Windows;
using RMDesktopUI.Library.Models;
using AutoMapper;

namespace RMDesktopUI.ViewModels
{
    // Caliburn Micro wires up UI and logic behind the scene, based on names.
    public class LoginViewModel : Screen
    {
        private string _email = "Matija@gmail.com";
        private string _password = "matija123";
        private IAPIHelper _apiHelper;
        private IEventAggregator _events;
        private readonly IUserEndpoint _userEndpoint;
        private ILoggedInUserModel _loggedInUser;
        private IAutoMapper _autoMapper;
        private readonly IPasswordEncryptor _passwordEncryptor;

        public LoginViewModel(IAPIHelper apiHelper, IEventAggregator events, IUserEndpoint userEndpoint, ILoggedInUserModel loggedInUser, IAutoMapper autoMapper, IPasswordEncryptor passwordEncryptor)
        {
            _apiHelper = apiHelper;
            _events = events;
            _userEndpoint = userEndpoint;
            _loggedInUser = loggedInUser;
            _autoMapper = autoMapper;
            _passwordEncryptor = passwordEncryptor;
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                NotifyOfPropertyChange(() => IsBusy);
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                // Indicates if UserName property changed value.
                NotifyOfPropertyChange(() => Email);
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
                if (Email?.Length > 0 && Password?.Length > 0)
                {
                    output = true;
                }
                return output;
            }
        }        

        public bool IsErrorVisible
        {
            get
            {
                bool output = false;
                if (ErrorMessage?.Length > 0)
                {
                    output = true;
                }
                return output;
            }
          
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {                
                _errorMessage = value;
                NotifyOfPropertyChange(() => IsErrorVisible);   
                NotifyOfPropertyChange(() => ErrorMessage);
            }
        }

        public async void LogIn()
        {
                ErrorMessage = "";

                IsBusy = true;

                var ExistingUser = await _userEndpoint.GetUserByEmail(_email);
                string ExistingUsersPassword;

                if (ExistingUser == null)
                {
                    ErrorMessage = "User does not exist";
                }
                else 
                {
                    ExistingUsersPassword = _passwordEncryptor.Decrypt(ExistingUser.Password);

                    if (ExistingUsersPassword != _password)
                    {
                        ErrorMessage = "Incorrect password";
                    }
                    else
                    {
                        LoggedInUserModel loggedInUser = Mapper.Map<LoggedInUserModel>(ExistingUser);
                        _loggedInUser.PopulateLoggedInUser(loggedInUser);

                        switch (_loggedInUser.Role)
                        {
                            case "Cashier":
                                _events.PublishOnUIThread(new CashierLogOnEvent());
                                break;
                            case "Manager":
                                _events.PublishOnUIThread(new ManagerLogOnEvent());
                                break;
                            case "CEO":
                                _events.PublishOnUIThread(new CEOLogOnEvent());
                                break;
                        }
                    }                             
                }

                IsBusy = false;         
        }
    }
}
