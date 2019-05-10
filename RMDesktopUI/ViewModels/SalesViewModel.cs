using Caliburn.Micro;
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
    public class SalesViewModel : Screen
    {
        IProductEndpoint _productEndpoint;

        public SalesViewModel(IProductEndpoint productEndpoint)
        {
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

        private int _itemQuantity;

        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set
            {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
            }
        }

        private BindingList<ProductModel> _cart;

        public BindingList<ProductModel> Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        public bool CanAddToCart
        {
            get
            {
                bool output  = false;

             // Make sure something is selected
             // Make sure item quantity is not 0

                return output;
            }
        }

        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;
              
                    // Make sure something is selected 

                return output;
            }
        }

        public bool CanCheckOut
        {
            get
            {
                bool output = false;

                // Make sure there is something in the cart

                return output;
            }
        }    

        public string SubTotal
        {
            get
            {
                // Replace with calculation
                return "$0.00";
            }
        }

        public string Tax
        {
            get
            {
                // Replace with calculation
                return "$0.00";
            }
        }

        public string Total
        {
            get
            {
                // Replace with calculation
                return "$0.00";
            }
        }


        public void AddToCart()
        {

        }

        public void CheckOut()
        {

        }

        public void RemoveFromCart()
        {

        }

        private async Task LoadProducts()
        {
            var productList = await _productEndpoint.GetAll();
            Products = new BindingList<ProductModel>(productList);
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }

       



    }
}
