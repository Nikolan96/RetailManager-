﻿using Caliburn.Micro;
using RMDesktopUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMDesktopUI.Library.Api;
using RMDesktopUI.EventModels;

namespace RMDesktopUI.ViewModels
{
    // Caliburn Micro wires up UI and logic behind the scene, based on names.
    public class LoginViewModel : Screen
    {
        private string _userName = "Nikolan96@gmail.com";
        private string _password = "Engeliousqw13!";
        private IAPIHelper _apiHelper;
        private IEventAggregator _events;

        public LoginViewModel(IAPIHelper apiHelper, IEventAggregator events)
        {
            _apiHelper = apiHelper;
            _events = events;
        }

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




        // 
        public async Task LogIn()
        {
            try
            {
                ErrorMessage = "";
                var result = await _apiHelper.Authenticate(UserName, Password);

                await _apiHelper.GetLoggedInUserInfo(result.Access_Token);

                // Raises an event for Login
                _events.PublishOnUIThread(new LogOnEvent());
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
           
        }




    }
}
