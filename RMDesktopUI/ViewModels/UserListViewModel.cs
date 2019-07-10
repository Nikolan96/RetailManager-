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
using System.Windows;

namespace RMDesktopUI.ViewModels
{
    public class UserListViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IUserEndpoint _userEndpoint;
        private readonly IPasswordEncryptor _passwordEncryptor;
        private readonly IShopEndpoint _shopEndpoint;

        public UserListViewModel(IEventAggregator eventAggregator, IUserEndpoint userEndpoint, IPasswordEncryptor passwordEncryptor,
            IShopEndpoint shopEndpoint)
        {
            _eventAggregator = eventAggregator;
            _userEndpoint = userEndpoint;
            _passwordEncryptor = passwordEncryptor;
            _shopEndpoint = shopEndpoint;
        }

        private string _firstNameTb;

        public string FirstNameTb
        {
            get { return _firstNameTb; }
            set
            {
                _firstNameTb = value;
                NotifyOfPropertyChange(() => FirstNameTb);
                NotifyOfPropertyChange(() => CanAddUser);
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
                NotifyOfPropertyChange(() => CanAddUser);

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
                NotifyOfPropertyChange(() => CanAddUser);

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
                NotifyOfPropertyChange(() => CanAddUser);

            }
        }

        private string _role;

        public string Role
        {
            get { return _role; }
            set
            {
                _role = value;
                NotifyOfPropertyChange(() => Role);
                NotifyOfPropertyChange(() => CanAddUser);

            }
        }

        private int _shopId;

        public int ShopId
        {
            get { return _shopId; }
            set
            {
                _shopId = value;
                NotifyOfPropertyChange(() => ShopId);
                NotifyOfPropertyChange(() => CanAddUser);

            }
        }

        private BindingList<UserModel> _users;

        public BindingList<UserModel> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
                NotifyOfPropertyChange(() => Users);
            }
        }

        private UserModel _selectedUser;

        public UserModel SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                NotifyOfPropertyChange(() => SelectedUser);
                NotifyOfPropertyChange(() => CanEditUser);
                NotifyOfPropertyChange(() => CanDeleteUser);
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

                if (SelectedUser != null)
                {
                    output = true;
                }

                return output;
            }
        }

        public void EditUser()
        {
            _eventAggregator.PublishOnUIThread(new EditUserViewEvent(_selectedUser));
        }

        public bool CanDeleteUser
        {
            get
            {
                bool output = false;

                if (SelectedUser != null && SelectedUser.Role != "CEO")
                {
                    output = true;
                }

                return output;
            }
        }

        public async void DeleteUser()
        {
            int ID = SelectedUser.ID;
            await _userEndpoint.DeleteUser(ID);

            ShopModel shop = await _shopEndpoint.GetShopById(SelectedUser.ShopId);

            UpdateShopNumOfEmployeesModel numOfEmployees = new UpdateShopNumOfEmployeesModel()
            {
                ID = SelectedUser.ShopId,
                NumOfEmployees = shop.NumOfEmployees - 1
            };

            await _shopEndpoint.UpdateShopNumOfEmployees(numOfEmployees);

            await LoadUsers();
        }

        public bool CanAddUser
        {
            get
            {
                bool output = false;

                if (!string.IsNullOrWhiteSpace(FirstNameTb)  && !string.IsNullOrWhiteSpace(LastNameTb) &&
                    !string.IsNullOrWhiteSpace(PasswordTb) && !string.IsNullOrWhiteSpace(EmailTb) && 
                    Role != null)
                {
                    output = true;
                }

                return output;
            }
        }

        public async void AddUser()
        {
            InsertUserModel user = new InsertUserModel()
            {
                FirstName = FirstNameTb,
                LastName = LastNameTb,
                EmailAddress = EmailTb,
                Role = Role,
                ShopId = ShopId,
                Password = _passwordEncryptor.Encrypt(PasswordTb)              
            };

            UserModel existingUser = await _userEndpoint.GetUserByEmail(user.EmailAddress);

            if (existingUser == null)
            {     
                await _userEndpoint.InsertUser(user);

                ShopModel shop = await _shopEndpoint.GetShopById(user.ShopId);

                UpdateShopNumOfEmployeesModel numOfEmployees = new UpdateShopNumOfEmployeesModel()
                {
                    ID = user.ShopId,
                    NumOfEmployees = shop.NumOfEmployees + 1
                };

                await _shopEndpoint.UpdateShopNumOfEmployees(numOfEmployees);
                ResetTextboxes();
                await LoadUsers();
            }
            else
            {
                MessageBox.Show($"User with {user.EmailAddress} email address already exists.");
                EmailTb = "";
            }
        }

        public void ResetTextboxes()
        {
            FirstNameTb = "";
            LastNameTb = "";
            EmailTb = "";
            PasswordTb = "";
            Role = null;
            ShopId = 1;
        }

        public async Task LoadShopIds()
        {
            var ListOfShopIds = await _shopEndpoint.GetShopIds();
            ShopIds = new BindingList<int>(ListOfShopIds);
        }

        private async Task LoadUsers()
        {
            var usersList = await _userEndpoint.GetUsers();

            foreach (var user in usersList)
            {
                user.Password = _passwordEncryptor.Decrypt(user.Password);
            }

            Users = new BindingList<UserModel>(usersList);
        }

        private void LoadRoles()
        {
            BindingList<string> ListOfRoles = new BindingList<string>()
            {
                "Manager",
                "Cashier",
                "CEO"
            };

            Roles = new BindingList<string>(ListOfRoles);
        }

        public void GoToCEOMenu()
        {
            _eventAggregator.PublishOnUIThread(new CEOLogOnEvent());
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadUsers();
            LoadRoles();
            await LoadShopIds();
        }

    }
}
