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
    public class OrderFormViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IProductEndpoint _productEndpoint;
        private readonly ILoggedInUserModel _loggedInUser;

        public OrderFormViewModel(IEventAggregator eventAggregator, IProductEndpoint productEndpoint, ILoggedInUserModel loggedInUser)
        {
            _eventAggregator = eventAggregator;
            _productEndpoint = productEndpoint;
            _loggedInUser = loggedInUser;
        }

        private BindingList<ProductNameQuantityModel> _products;

        public BindingList<ProductNameQuantityModel> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        private BindingList<string> _productNames = new BindingList<string>();

        public BindingList<string> ProductNames
        {
            get { return _productNames; }
            set
            {
                _productNames = value;
                NotifyOfPropertyChange(() => ProductNames);
            }
        }

        private string _selectedProductName;

        public string SelectedProductName
        {
            get { return _selectedProductName; }
            set
            {
                _selectedProductName = value;
                CurrentQuantityTb = GetQuantityByName(value);
                NotifyOfPropertyChange(() => SelectedProductName);
                NotifyOfPropertyChange(() => CurrentQuantityTb);
                NotifyOfPropertyChange(() => CanAddOrder);
            }
        }

        private int _currentQuantityTb;

        public int CurrentQuantityTb
        {
            get { return _currentQuantityTb; }
            set
            {
                _currentQuantityTb = value;
                NotifyOfPropertyChange(() => CurrentQuantityTb);
            }
        }

        private int _quantityTb;

        public int QuantityTb
        {
            get { return _quantityTb; }
            set
            {
                _quantityTb = value;
                NotifyOfPropertyChange(() => QuantityTb);
                NotifyOfPropertyChange(() => CanAddOrder);
            }
        }


        public int GetQuantityByName(string SelectedProductName)
        {
            int value = 0;

            foreach (var product in Products)
            {
                if (product.ProductName == SelectedProductName)
                {
                    value = product.QuantityInStock;
                }
            }

            return value;
        }

        public bool CanAddOrder
        {
            get
            {
                bool output = false;

                if (SelectedProductName != null && QuantityTb == 0)
                {
                    output = true;
                }

                return output;
            }
        }

        public void AddOrder()
        {

        }

        public bool CanCancelOrderItem
        {
            get
            {
                bool output = false;

                // if SelectedOrderItem != null
                //if (true)
                //{
                //    output = true;
                //}

                return output;
            }
        }

        public bool CanOrder
        {
            get
            {
                bool output = false;

                // if OrderItems != null
                //if (true)
                //{ 
                //    output = true;
                //}

                return output;
            }
        }

        public void Order()
        {
            // add Order to db, Add orderItems to db, clear OrderItems
            _eventAggregator.PublishOnUIThread(new ProductsViewEvent());
        }

        public void CancelOrderItem()
        {

        }

        private async Task LoadProducts()
        {
            var productsAndQuantities = await _productEndpoint.GetProductNamesAndQuantities(_loggedInUser.ShopId);
            Products = new BindingList<ProductNameQuantityModel>(productsAndQuantities);

            foreach (var product in Products)
            {
                ProductNames.Add(product.ProductName);
            }

        }

        private async Task LoadCurrentQuantity()
        {
            CurrentQuantityTb = await _productEndpoint.GetQuantityOfProductByName(SelectedProductName);
            NotifyOfPropertyChange(() => CurrentQuantityTb);
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }

        public void GoToProductsView()
        {
            _eventAggregator.PublishOnUIThread(new ProductsViewEvent());
        }
    }
}
