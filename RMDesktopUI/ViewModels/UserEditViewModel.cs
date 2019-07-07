using Caliburn.Micro;
using RMDesktopUI.EventModels;
using RMDesktopUI.Helpers;
using RMDesktopUI.Library.Api;
using RMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
    public class UserEditViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IUserEndpoint _userEndpoint;
        private readonly IShopEndpoint _shopEndpoint;
        private readonly IPasswordEncryptor _passwordEncryptor;

        public UserModel _user { get; set; }

        public UserEditViewModel(IEventAggregator eventAggregator, IUserEndpoint userEndpoint, IShopEndpoint shopEndpoint,
            IPasswordEncryptor passwordEncryptor)
        {
            _eventAggregator = eventAggregator;
            _userEndpoint = userEndpoint;
            _shopEndpoint = shopEndpoint;
            _passwordEncryptor = passwordEncryptor;
        }

        private string _firstNameTb;

        public string FirstNameTb
        {
            get { return _firstNameTb; }
            set
            {
                _firstNameTb = value;
                NotifyOfPropertyChange(() => FirstNameTb);
                NotifyOfPropertyChange(() => CanEditUser);
            }
        }

        private string _lastNameTb;

        public string LastNameTb
        {
            get { return _lastNameTb; }
            set
            {
                _lastNameTb = value;
                NotifyOfPropertyChange(() => LastNameTb);
                NotifyOfPropertyChange(() => CanEditUser);
            }
        }

        private string _emailTb;

        public string EmailTb
        {
            get { return _emailTb; }
            set
            {
                _emailTb = value;
                NotifyOfPropertyChange(() => EmailTb);
                NotifyOfPropertyChange(() => CanEditUser);
            }
        }

        private string _passwordTb;

        public string PasswordTb
        {
            get { return _passwordTb; }
            set
            {
                _passwordTb = value;
                NotifyOfPropertyChange(() => PasswordTb);
                NotifyOfPropertyChange(() => CanEditUser);
            }
        }

        private string _selectedRole;

        public string SelectedRole
        {
            get { return _selectedRole; }
            set
            {
                _selectedRole = value;
                NotifyOfPropertyChange(() => SelectedRole);
                NotifyOfPropertyChange(() => CanEditUser);
            }
        }

        private int _selectedShopId;

        public int SelectedShopId
        {
            get { return _selectedShopId; }
            set
            {
                _selectedShopId = value;
                NotifyOfPropertyChange(() => SelectedShopId);
                NotifyOfPropertyChange(() => CanEditUser);
            }
        }

        private BindingList<string> _roles;

        public BindingList<string> Roles
        {
            get { return _roles; }
            set
            {
                _roles = value;
                NotifyOfPropertyChange(() => Roles);
            }
        }

        private BindingList<int> _shopIds;

        public BindingList<int> ShopIds
        {
            get { return _shopIds; }
            set
            {
                _shopIds = value;
                NotifyOfPropertyChange(() => ShopIds);
            }
        }

        public bool CanEditUser
        {
            get
            {
                bool output = false;

                if (!string.IsNullOrWhiteSpace(FirstNameTb) && !string.IsNullOrWhiteSpace(LastNameTb) && !string.IsNullOrWhiteSpace(EmailTb)
                    && !string.IsNullOrWhiteSpace(PasswordTb))
                {
                    output = true;
                }

                return output;
            }
        }

        public async void EditUser()
        {
            UpdateUserModel updateUser = new UpdateUserModel()
            {
                ID = _user.ID,
                FirstName = FirstNameTb,
                LastName = LastNameTb,
                EmailAddress = EmailTb,
                Password = _passwordEncryptor.Encrypt(PasswordTb),
                Role = SelectedRole,
                ShopId = SelectedShopId
            };

            await _userEndpoint.UpdateUser(updateUser);
            _eventAggregator.PublishOnUIThread(new UserListViewEvent());
        }

        public async Task LoadShopIds()
        {
            var ListOfShopIds = await _shopEndpoint.GetShopIds();
            ShopIds = new BindingList<int>(ListOfShopIds);
        }

        public void LoadRoles()
        {
            var ListOfRoles = new List<string>()
            {
                "Cashier",
                "Manager"
            };
            Roles = new BindingList<string>(ListOfRoles);
        }

        public void LoadTextboxes()
        {
            FirstNameTb = _user.FirstName;
            LastNameTb = _user.LastName;
            EmailTb = _user.EmailAddress;
            PasswordTb = _user.Password;
            SelectedRole = _user.Role;
            SelectedShopId = _user.ShopId;
        }

        public void AddUserModel(UserModel User)
        {
            _user = User;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            LoadRoles();
            await LoadShopIds();
            LoadTextboxes();
        }

        public void Back()
        {
            _eventAggregator.PublishOnUIThread(new UserListViewEvent());
        }

    }
}
