using Caliburn.Micro;
using RMDesktopUI.EventModels;
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
    public class ShopListViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IShopEndpoint _shopEndpoint;

        public ShopListViewModel(IEventAggregator eventAggregator, IShopEndpoint shopEndpoint)
        {
            _eventAggregator = eventAggregator;
            _shopEndpoint = shopEndpoint;
        }

        private string _town;

        public string Town
        {
            get { return _town; }
            set
            {
                _town = value;
                NotifyOfPropertyChange(() => Town);
                NotifyOfPropertyChange(() => CanAddShop);
            }
        }

        private string _address;

        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                NotifyOfPropertyChange(() => Address);
                NotifyOfPropertyChange(() => CanAddShop);
            }
        }

        private ShopModel _selectedShop;

        public ShopModel SelectedShop
        {
            get { return _selectedShop; }
            set
            {
                _selectedShop = value;
                NotifyOfPropertyChange(() => SelectedShop);
                NotifyOfPropertyChange(() => CanDeleteShop);
                NotifyOfPropertyChange(() => CanEditShop);
            }
        }


        private BindingList<ShopModel> _shops;

        public BindingList<ShopModel> Shops
        {
            get { return _shops; }
            set
            {
                _shops = value;
                NotifyOfPropertyChange(() => Shops);
            }
        }

        public void EditShop()
        {
            _eventAggregator.PublishOnUIThread(new EditShopViewEvent(_selectedShop));
        }

        public bool CanEditShop
        {
            get
            {
                bool output = false;

                if (SelectedShop != null)
                {
                    output = true;
                }

                return output;
            }
        }

        public async void DeleteShop()
        {
            int ShopID = SelectedShop.ID;
            await _shopEndpoint.DeleteShop(ShopID);
            await LoadShops();
        }

        public bool CanDeleteShop
        {
            get
            {
                bool output = false;

                if (SelectedShop != null)
                {
                    output = true;
                }

                return output;
            }
        }

        public async void AddShop()
        {
            var ShopToAdd = new InsertShopModel() { Town = Town, Address = Address };
            await _shopEndpoint.InsertShop(ShopToAdd);
            ResetTextboxes();
            await LoadShops();
        }

        public bool CanAddShop
        {           
            get
            {
                bool output = false;

                if (!string.IsNullOrWhiteSpace(Address) && !string.IsNullOrWhiteSpace(Town))
                {
                    return true;
                }

                return output;
            }    
        }

        public void GoToCEOMenu()
        {
            _eventAggregator.PublishOnUIThread(new CEOLogOnEvent());
        }

        public void ResetTextboxes()
        {
            Town = "";
            Address = "";
        }

        public async Task LoadShops()
        {
            var ListOfShops = await _shopEndpoint.GetShops();
            Shops = new BindingList<ShopModel>(ListOfShops);
        }


        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadShops();
        }
    }
}
