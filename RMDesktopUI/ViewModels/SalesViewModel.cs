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
using System.Windows;

namespace RMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        IProductEndpoint _productEndpoint;
        private readonly IEventAggregator _events;

        public SalesViewModel(IProductEndpoint productEndpoint, IEventAggregator events)
        {
            _productEndpoint = productEndpoint;
            _events = events;
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

        private int _itemQuantity = 1;

        public int ItemQuantity
        {
            get
            {
                return _itemQuantity;
            }
            set
            {
                _itemQuantity = value;

                NotifyOfPropertyChange(() => CanAddToCart);
                NotifyOfPropertyChange(() => ItemQuantity);
                
            }
        }

        private ProductModel _selectedProduct;

        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart);               
            }
        }

        private CartItemModel _selectedItemFromCart;

        public CartItemModel SelectedItemFromCart
        {
            get { return _selectedItemFromCart; }
            set
            {
                _selectedItemFromCart = value;
                NotifyOfPropertyChange(() => SelectedItemFromCart);
                NotifyOfPropertyChange(() => CanRemoveFromCart);
            }

        }

        private BindingList<CartItemModel> _cart = new BindingList<CartItemModel>();

        public BindingList<CartItemModel> Cart
        {
            get { return _cart; }
            set 
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
                NotifyOfPropertyChange(() => CanCheckOut);
            }
        }

        public bool CanAddToCart
        {
            get
            {
                bool output  = false;

                if (ItemQuantity > 0 && SelectedProduct?.QuantityInStock >= ItemQuantity)
                {
                    output = true;
                }
           
                return output;
            }
        }

        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;

                if (SelectedItemFromCart != null)
                {
                    output = true;
                }

                return output;
            }
        }

        public bool CanCheckOut
        {
            get
            {
                bool output = false;

                if (Cart.Count > 0)
                {
                    output = true;
                }

                return output;
            }
        }    

        public string SubTotal
        {
            get
            {
                decimal subTotal = 0;

                foreach (var item in Cart)
                {
                    subTotal += item.Product.RetailPrice * item.QuantityInCart;
                }

                return subTotal.ToString();
            }
        }

        public string DisplaySubTotal
        {
            get
            {
                decimal value = Convert.ToDecimal(SubTotal);
                value = Decimal.Round(value, 2);
                return value.ToString();
            }
        }

        public string Tax
        {
            get
            {
                decimal tax = 0;

                foreach (var item in Cart)
                {
                    tax += ((item.Product.RetailPrice * item.Product.Tax) / 100) * item.QuantityInCart;
                }

                return tax.ToString();
            }
        }

        public string DisplayTax
        {
            get
            {
                decimal value = Convert.ToDecimal(Tax);
                value = Decimal.Round(value, 2);
                return value.ToString();
            }
        }

        public string Total
        {
            get
            {
                decimal total = 0;

                total = Convert.ToDecimal(SubTotal) + Convert.ToDecimal(Tax);

                return total.ToString();

            }
        }

        public string DisplayTotal
        {
            get
            {
                decimal value = Convert.ToDecimal(Total);
                value = Decimal.Round(value, 2);
                return value.ToString();
            }
        }


        public void AddToCart()
        {
            CartItemModel existingItem = Cart.FirstOrDefault(x => x.Product == SelectedProduct);

            if (existingItem != null)
            {
                existingItem.QuantityInCart += ItemQuantity;

                Cart.Remove(existingItem);
                Cart.Add(existingItem);
                NotifyOfPropertyChange(() => CanCheckOut);
            }
            else
            {
                CartItemModel item = new CartItemModel
                {
                    Product = SelectedProduct,
                    QuantityInCart = ItemQuantity
                };
                Cart.Add(item);
            }
          
            SelectedProduct.QuantityInStock -= ItemQuantity;
            ItemQuantity = 1;
            SelectedProduct = null;
            NotifyOfPropertyChange(() => DisplaySubTotal);
            NotifyOfPropertyChange(() => DisplayTax);
            NotifyOfPropertyChange(() => DisplayTotal);
            NotifyOfPropertyChange(() => CanCheckOut);
            NotifyOfPropertyChange(() => CanRemoveAllFromCart);
        }     

        public void RemoveFromCart()
        {
            CartItemModel ItemToRemove = Cart.FirstOrDefault(x => x.Product.ProductName == SelectedItemFromCart.Product.ProductName);

            foreach (var product in Products)
            {
                if (product.ProductName == ItemToRemove.Product.ProductName)
                {
                    product.QuantityInStock += ItemToRemove.QuantityInCart;
                }
            }

            Cart.Remove(SelectedItemFromCart);

            SelectedItemFromCart = null;
            ItemQuantity = 1;
            NotifyOfPropertyChange(() => DisplaySubTotal);
            NotifyOfPropertyChange(() => DisplayTax);
            NotifyOfPropertyChange(() => DisplayTotal);
            NotifyOfPropertyChange(() => CanCheckOut);
            NotifyOfPropertyChange(() => CanRemoveAllFromCart);
        }

        public bool CanRemoveAllFromCart
        {
            get
            {
                bool output = false;

                if (Cart.Count > 0)
                {
                    output = true;
                }

                return output;
            }
        }

        public void RemoveAllFromCart()
        {
            foreach (var item in Cart)
            {
                foreach (var product in Products)
                {
                    var prod = item.Product.ProductName;
                    if (product.ProductName == prod)
                    {
                        product.QuantityInStock += item.QuantityInCart;
                    }                 
                }               
            }

            Cart.Clear();

            ItemQuantity = 1;
            NotifyOfPropertyChange(() => DisplaySubTotal);
            NotifyOfPropertyChange(() => DisplayTax);
            NotifyOfPropertyChange(() => DisplayTotal);
            NotifyOfPropertyChange(() => CanCheckOut);
            NotifyOfPropertyChange(() => CanRemoveAllFromCart);
        }

        public void CheckOut()
        {
            string items = "";

            foreach (var item in Cart)
            {
                items += item.DisplayText + "\n";
            }

            items += "\n";
            items += $"SubTotal : {DisplaySubTotal} \n";
            items += $"Tax : {DisplayTax} \n";
            items += $"Total : {DisplayTotal} \n";

            MessageBox.Show(items);
        }

        public void GoToCashRegister()
        {
            _events.PublishOnUIThread(new CashRegisterEvent());
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
