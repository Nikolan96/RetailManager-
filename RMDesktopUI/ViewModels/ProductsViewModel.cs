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
    public class ProductsViewModel : Screen
    {

        private IAPIHelper _apiHelper;
        private IEventAggregator _events;
        private readonly IProductEndpoint _productEndpoint;

        public ProductsViewModel(IAPIHelper apiHelper, IEventAggregator events, IProductEndpoint productEndpoint)
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

        public void GoToCashRegister()
        {
            _events.PublishOnUIThread(new CashRegisterEvent());
        }

        private async Task LoadProducts()
        {
            var productList = await _productEndpoint.GetAll();

            foreach (var item in productList)
            {
                if (item.QuantityInStock <= 20)
                {
                    item.Color = "Red";
                }
                else
                    item.Color = "Black";
            }

            Products = new BindingList<ProductModel>(productList);
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }


    }

}
