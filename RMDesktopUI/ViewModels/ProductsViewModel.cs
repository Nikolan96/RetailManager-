﻿using Caliburn.Micro;
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
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace RMDesktopUI.ViewModels
{
    public class ProductsViewModel : Screen
    {

        private IAPIHelper _apiHelper;
        private IEventAggregator _events;
        private readonly IProductEndpoint _productEndpoint;
        private readonly ILoggedInUserModel _loggedInUser;

        public ProductsViewModel(IAPIHelper apiHelper, IEventAggregator events, IProductEndpoint productEndpoint, ILoggedInUserModel loggedInUser)
        {
            _apiHelper = apiHelper;
            _events = events;
            _productEndpoint = productEndpoint;
            _loggedInUser = loggedInUser;
        }

        private string _productIDTb;

        public string ProductIDTb
        {
            get { return _productIDTb; }
            set
            {
                _productIDTb = value;
                NotifyOfPropertyChange(() => CanAddProduct);
                NotifyOfPropertyChange(() => ProductIDTb);
            }
        }

        private string _productNameTb;

        public string ProductNameTb
        {
            get { return _productNameTb; }
            set
            {
                _productNameTb = value;
                NotifyOfPropertyChange(() => CanAddProduct);
                NotifyOfPropertyChange(() => ProductNameTb);
            }
        }

        private string _categoryTb;

        public string CategoryTb
        {
            get { return _categoryTb; }
            set
            {
                _categoryTb = value;
                NotifyOfPropertyChange(() => CanAddProduct);
                NotifyOfPropertyChange(() => CategoryTb);
            }
        }

        private string _descriptionTb;

        public string DescriptionTb
        {
            get { return _descriptionTb; }
            set
            {
                _descriptionTb = value;
                NotifyOfPropertyChange(() => CanAddProduct);
                NotifyOfPropertyChange(() => DescriptionTb);
            }
        }

        private decimal _purchasePriceTb;

        public decimal PurchasePriceTb
        {
            get { return _purchasePriceTb; }
            set
            {
                _purchasePriceTb = value;
                NotifyOfPropertyChange(() => CanAddProduct);
                NotifyOfPropertyChange(() => PurchasePriceTb);
            }
        }

        private decimal _retailPriceTb;

        public decimal RetailPriceTb
        {
            get { return _retailPriceTb; }
            set
            {
                _retailPriceTb = value;
                NotifyOfPropertyChange(() => CanAddProduct);
                NotifyOfPropertyChange(() => RetailPriceTb);
            }
        }

        private decimal _taxTb;

        public decimal TaxTb
        {
            get { return _taxTb; }
            set
            {
                _taxTb = value;
                NotifyOfPropertyChange(() => CanAddProduct);
                NotifyOfPropertyChange(() => TaxTb);
            }
        }

        private int _quantityTb = 1;

        public int QuantityTb
        {
            get { return _quantityTb; }
            set
            {
                _quantityTb = value;
                NotifyOfPropertyChange(() => CanAddProduct);
                NotifyOfPropertyChange(() => QuantityTb);
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
                NotifyOfPropertyChange(() => CanDelete);
                NotifyOfPropertyChange(() => CanEdit);
            }
        }

        private string _isManager;

        public string IsManager
        {
            get { return _isManager; }
            set
            {
                _isManager = value;
                NotifyOfPropertyChange(() => IsManager);
            }
        }

        private int _colSpan;

        public int ColSpan
        {
            get { return _colSpan; }
            set
            {
                _colSpan = value;
                NotifyOfPropertyChange(() => ColSpan);
            }
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

        public async Task AddProduct()
        {
            ProductModel existingProduct = await _productEndpoint.GetProductByID(ProductIDTb);

            if (existingProduct != null)
            {               
                MessageBox.Show($"Product with ID : {ProductIDTb} already exists!");
                ProductIDTb = "";
            }
            else
            {
                InsertProductModel productModel = new InsertProductModel()
                {
                    ID = ProductIDTb,
                    ProductName = ProductNameTb,
                    Category = CategoryTb,
                    Description = DescriptionTb,
                    PurchasePrice = PurchasePriceTb,
                    RetailPrice = RetailPriceTb,
                    Tax = TaxTb,
                    Quantity = QuantityTb,
                    ShopID = _loggedInUser.ShopId
                };

                await _productEndpoint.InsertProduct(productModel);
                ResetTextboxes();

                await LoadProducts();
            }         
        }

        public bool CanAddProduct
        {
            get
            {
                bool output = false;

                if (RetailPriceTb > 0 && PurchasePriceTb > 0 && TaxTb > 0 && TaxTb <= 100 && QuantityTb > 0 && !string.IsNullOrWhiteSpace(ProductIDTb) &&
                    !string.IsNullOrWhiteSpace(ProductNameTb) && !string.IsNullOrWhiteSpace(CategoryTb) && !string.IsNullOrWhiteSpace(DescriptionTb))
                   
                {
                    output = true;
                }

                return output;
            }
        }

        public bool CanDelete
        {
            get
            {
                bool output = false;
                if (SelectedProduct != null)
                {
                    output = true;
                }
                return output;
            }
        }

        public bool CanEdit
        {
            get
            {
                bool output = false;
                if (SelectedProduct != null)
                {
                    output = true;
                }
                return output;
            }
        }

        public void ResetTextboxes()
        {
            RetailPriceTb = 0;
            PurchasePriceTb = 0;
            TaxTb = 0;
            ProductNameTb = "";
            CategoryTb = "";
            DescriptionTb = "";
            QuantityTb = 1;
        }

        public void Edit()
        {
            _events.PublishOnUIThread(new EditProductViewEvent(_selectedProduct));
        }

        public async Task Delete()
        {
            await _productEndpoint.DeleteProduct(SelectedProduct.ID);
            await LoadProducts();
        }

        public void GoToCashRegister()
        {
            _events.PublishOnUIThread(new CashRegisterEvent());
        }

        public void Scanner()
        {
            _events.PublishOnUIThread(new ScannerViewEvent(new ProductsViewEventWithScanResult()));
        }

        public void Order()
        {
            _events.BeginPublishOnUIThread(new OrdersFormViewEvent());
        }

        public void Orders()
        {
            _events.BeginPublishOnUIThread(new OrdersViewEvent());
        }

        private async Task LoadProducts()
        {
            var productList = await _productEndpoint.GetProductsByShopID(_loggedInUser.ShopId);

            Products = new BindingList<ProductModel>(productList);
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();

            if (_loggedInUser.Role == "Manager")
            {
                IsManager = "Visible";
                ColSpan = 5;
            }
            else
            {
                IsManager = "Hidden";
                ColSpan = 8;
            }

            NotifyOfPropertyChange(() => ColSpan);
            NotifyOfPropertyChange(() => IsManager);
        }
    }
}
