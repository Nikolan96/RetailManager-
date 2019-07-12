using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using RMDesktopUI.EventModels;
using RMDesktopUI.Helpers;
using RMDesktopUI.Library.Api;
using RMDesktopUI.Library.Models;

namespace RMDesktopUI.ViewModels
{
    // Conductor holds on to and activates only one item at a time.
    public class ShellViewModel : Conductor<object>, IHandle<CashierLogOnEvent>, IHandle<CashRegisterEvent>, IHandle<ProductsViewEvent>, IHandle<EditProductViewEvent>, IHandle<BillsViewEvent>, IHandle<BillItemsViewEvent>,
        IHandle<ManagerLogOnEvent>, IHandle<CEOLogOnEvent>, IHandle<ChartMenuViewEvent>, IHandle<ShopListViewEvent>, IHandle<UserListViewEvent>, IHandle<LogoutEvent>, IHandle<EditShopViewEvent>, IHandle<EditUserViewEvent>
    {
        private readonly IEventAggregator _events;
        private readonly IAutoMapper _autoMapper;
        private readonly SalesViewModel _salesVM;
        private readonly SimpleContainer _container;
        private readonly ProductsViewModel _productsVM;
        private readonly EditProductViewModel _editProductViewModel;
        private readonly BillsViewModel _billsViewModel;
        private readonly BillItemsViewModel _billItemsViewModel;
        private readonly ManagerMenuViewModel _managerMenuViewModel;
        private readonly CEOMenuViewModel _ceoMenuViewModel;
        private readonly ChartMenuViewModel _chartMenuViewModel;
        private readonly UserListViewModel _userListViewModel;
        private readonly ShopListViewModel _shopListViewModel;
        private readonly ShopEditViewModel _shopEditViewModel;
        private readonly UserEditViewModel _userEditViewModel;
        private ILoggedInUserModel _loggedInUserModel;
        private readonly IUserEndpoint _userEndpoint;
        private readonly IPasswordEncryptor _passwordEncryptor;
        private readonly IShopEndpoint _shopEndpoint;
        private readonly CashRegisterViewModel _cashRegisterVM;

        // Uses constructor injection to pass in a new instance of LoginVM and activate it.
        public ShellViewModel(IEventAggregator events, IAutoMapper autoMapper, SalesViewModel salesVM, CashRegisterViewModel cashRegisterVM, SimpleContainer container,
            ProductsViewModel ProductsVM, EditProductViewModel editProductViewModel, BillsViewModel billsViewModel, BillItemsViewModel billItemsViewModel,
            ManagerMenuViewModel managerMenuViewModel, CEOMenuViewModel ceoMenuViewModel, ChartMenuViewModel chartMenuViewModel, UserListViewModel userListViewModel,
            ShopListViewModel shopListViewModel, ShopEditViewModel shopEditViewModel, UserEditViewModel userEditViewModel, ILoggedInUserModel loggedInUserModel, IUserEndpoint userEndpoint
            , IPasswordEncryptor passwordEncryptor, IShopEndpoint shopEndpoint)
        {
            _events = events;
            _autoMapper = autoMapper;
            _salesVM = salesVM;
            _cashRegisterVM = cashRegisterVM;
            _container = container;
            _productsVM = ProductsVM;
            _editProductViewModel = editProductViewModel;
            _billsViewModel = billsViewModel;
            _billItemsViewModel = billItemsViewModel;
            _managerMenuViewModel = managerMenuViewModel;
            _ceoMenuViewModel = ceoMenuViewModel;
            _chartMenuViewModel = chartMenuViewModel;
            _userListViewModel = userListViewModel;
            _shopListViewModel = shopListViewModel;
            _shopEditViewModel = shopEditViewModel;
            _userEditViewModel = userEditViewModel;
            _loggedInUserModel = loggedInUserModel;
            _userEndpoint = userEndpoint;
            _passwordEncryptor = passwordEncryptor;
            _shopEndpoint = shopEndpoint;
            _autoMapper.Initialize();

            // Subscribes instance of shellview to events
            _events.Subscribe(this);

            ActivateItem(_container.GetInstance<LoginViewModel>()); // gets new instance of LoginViewModel and places it in _loginVM ( cleanes info from last one ).
            
        }

        public void LogOut()
        {
            _loggedInUserModel = null;
            _events.PublishOnUIThread(new LogoutEvent());
        }

        public void Handle(CashierLogOnEvent message)
        {
            ActivateItem(_cashRegisterVM);
        }

        public void Handle(CashRegisterEvent messager)
        {
            ActivateItem(_cashRegisterVM);
        }

        public void Handle(ProductsViewEvent message)
        {
            ActivateItem(_productsVM);
        }

        public void Handle(EditProductViewEvent message)
        {
            _editProductViewModel.AddProductModel(message.SelectedProduct);
            ActivateItem(_editProductViewModel);
        }

        public void Handle(BillsViewEvent message)
        {
            ActivateItem(_billsViewModel);
        }

        public void Handle(BillItemsViewEvent message)
        {
            _billItemsViewModel.AddBillId(message.BillId);
            ActivateItem(_billItemsViewModel);
        }

        public void Handle(ManagerLogOnEvent message)
        {
            ActivateItem(_managerMenuViewModel);
        }

        public void Handle(CEOLogOnEvent message)
        {
            ActivateItem(_ceoMenuViewModel);
        }

        public void Handle(ChartMenuViewEvent message)
        {
            ActivateItem(_chartMenuViewModel);
        }

        public void Handle(UserListViewEvent message)
        {
            ActivateItem(_userListViewModel);
        }

        public void Handle(ShopListViewEvent message)
        {
            ActivateItem(_shopListViewModel);
        }

        public void Handle(LogoutEvent message)
        {

            ActivateItem(_container.GetInstance<LoginViewModel>());
        }

        public void Handle(EditShopViewEvent message)
        {
            _shopEditViewModel.AddShopModel(message.SelectedShop);
            ActivateItem(_shopEditViewModel);
        }

        public void Handle(EditUserViewEvent message)
        {
            _userEditViewModel.AddUserModel(message.SelectedUser);
            ActivateItem(_userEditViewModel);
        }

        public async void Seed()
        {
          
            InsertShopModel Shop1 = new InsertShopModel()
            {
                Town = "Novi Sad",
                Address = "Bulevar oslobodjenja 36"
            };

            InsertShopModel Shop2 = new InsertShopModel()
            {
                Town = "Beograd",
                Address = "Bulevar Vojvode Putnika 71"
            };

            List<InsertShopModel> shopModels = new List<InsertShopModel>()
            {
                Shop1, Shop2
            };

            foreach (var item in shopModels)
            {
                ShopModel ExistingShop = new ShopModel();
                ExistingShop = await _shopEndpoint.GetShopByAddress(item.Address);

                if (ExistingShop != null)
                {
                    await _shopEndpoint.InsertShop(item);
                }
            }

            InsertUserModel CEO = new InsertUserModel()
            {
                EmailAddress = "Nikolan96@gmail.com",
                FirstName = "Nikola",
                LastName = "Andrić",
                Password = _passwordEncryptor.Encrypt("Nikola123"),
                Role = "CEO",
                ShopId = 1
                
            };

            InsertUserModel Manager1 = new InsertUserModel()
            {
                EmailAddress = "Matija@gmail.com",
                FirstName = "Matija",
                LastName = "Andrić",
                Password = _passwordEncryptor.Encrypt("Matija123"),
                Role = "Manager",
                ShopId = 1
            };

            InsertUserModel Manager2 = new InsertUserModel()
            {
                EmailAddress = "Pera@gmail.com",
                FirstName = "Pera",
                LastName = "Perić",
                Password = _passwordEncryptor.Encrypt("Pera123"),
                Role = "Manager",
                ShopId = 2
            };

            InsertUserModel Cashier1 = new InsertUserModel()
            {
                EmailAddress = "Mika@gmail.com",
                FirstName = "Mika",
                LastName = "Mikić",
                Password = _passwordEncryptor.Encrypt("Mika123"),
                Role = "Cashier",
                ShopId = 1
            };

            InsertUserModel Cashier2 = new InsertUserModel()
            {
                EmailAddress = "Sanja@gmail.com",
                FirstName = "Sanja",
                LastName = "Marić",
                Password = _passwordEncryptor.Encrypt("Sanja123"),
                Role = "Cashier",
                ShopId = 2
            };

            List<InsertUserModel> insertUserModels = new List<InsertUserModel>()
            {
                CEO, Manager1, Manager2, Cashier1, Cashier2
            };

            foreach (var user in insertUserModels)
            {
                var existingUser = await _userEndpoint.GetUserByEmail(user.EmailAddress);

                if (existingUser != null)
                {
                    await _userEndpoint.InsertUser(user);
                }
            }

            
        }
    }
}
