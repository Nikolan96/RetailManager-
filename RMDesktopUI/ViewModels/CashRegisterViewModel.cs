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
    public class CashRegisterViewModel : Screen
    {
        private IAPIHelper _apiHelper;
        private IEventAggregator _events;
        private readonly IProductEndpoint _productEndpoint;

        public CashRegisterViewModel(IAPIHelper apiHelper, IEventAggregator events, IProductEndpoint productEndpoint)
        {
            _apiHelper = apiHelper;
            _events = events;
            _productEndpoint = productEndpoint;
        }

        private BindingList<ProductModel> _products;

        public BindingList<ProductModel> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        private BindingList<string> _productNames;

        public BindingList<string> ProductNames
        {
            get { return _productNames; }
            set
            {
                _productNames = value;
                NotifyOfPropertyChange(() => ProductNames);
            }
        }

        private async Task LoadProductNames()
        {
            //var productList = await _productEndpoint.GetAll();
            //ProductNames = new BindingList<string>(productList);

            var productNames = await _productEndpoint.GetProductNames();
            ProductNames = new BindingList<string>(productNames);
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProductNames();
        }

        public void GoToProductsView()
        {
            _events.PublishOnUIThread(new ProductsViewEvent());
        }

        public void GoToCashRegister()
        {
            _events.PublishOnUIThread(new CashRegisterEvent());
        }
    }
}
