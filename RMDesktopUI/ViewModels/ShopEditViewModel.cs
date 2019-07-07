using Caliburn.Micro;
using RMDesktopUI.EventModels;
using RMDesktopUI.Library.Api;
using RMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
    public class ShopEditViewModel : Screen
    {
        private readonly IShopEndpoint _shopEndpoint;
        private readonly IEventAggregator _eventAggregator;

        public ShopModel _shopModel{ get; set; }

        public ShopEditViewModel(IShopEndpoint shopEndpoint, IEventAggregator eventAggregator)
        {
            _shopEndpoint = shopEndpoint;
            _eventAggregator = eventAggregator;
        }

        private string _town;

        public string Town
        {
            get { return _town; }
            set
            {
                _town = value;
                NotifyOfPropertyChange(() => Town);
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
            }
        }

        public bool CanEditShop
        {
            get
            {
                bool output = false;

                if (!string.IsNullOrWhiteSpace(Town) && !string.IsNullOrWhiteSpace(Address))
                {
                    output = true;
                }

                return output;
            }           
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            FillTextboxes();
        }

        public async void EditShop()
        {
            var updateShopModel = new UpdateShopModel()
            {
                ID = _shopModel.ID,
                Town = Town,
                Address = Address
            };

            await _shopEndpoint.UpdateShop(updateShopModel);
            _eventAggregator.PublishOnUIThread(new ShopListViewEvent());
        }

        public void Back()
        {
            _eventAggregator.PublishOnUIThread(new ShopListViewEvent());
        }

        public void FillTextboxes()
        {      
            Town = _shopModel.Town;
            Address = _shopModel.Address;
        }

        public void AddShopModel(ShopModel shopModel)
        {
            _shopModel = shopModel;
        }
    }
}
