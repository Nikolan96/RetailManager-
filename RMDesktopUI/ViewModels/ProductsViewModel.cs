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

        public ProductsViewModel(IAPIHelper apiHelper, IEventAggregator events, IProductEndpoint productEndpoint)
        {
            _apiHelper = apiHelper;
            _events = events;
            _productEndpoint = productEndpoint;
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
            InsertProductModel productModel = new InsertProductModel();

            // should get automapper
            productModel.ProductName = ProductNameTb;
            productModel.Category = CategoryTb;
            productModel.Description = DescriptionTb;
            productModel.PurchasePrice = PurchasePriceTb;
            productModel.RetailPrice = RetailPriceTb;
            productModel.Tax = TaxTb;
            productModel.Quantity = QuantityTb;

            await _productEndpoint.InsertProduct(productModel);
            ResetTextboxes();

            await LoadProducts();       
        }

        public bool CanAddProduct
        {
            get
            {
                bool output = false;

                if (RetailPriceTb > 0 && PurchasePriceTb > 0 && TaxTb > 0 && TaxTb <= 100 && QuantityTb > 0 &&
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
           // Opens new windows and lets you edit selected product
        }

        public async Task Delete()
        {
            await _productEndpoint.DeleteProduct(SelectedProduct.Id);
            await LoadProducts();
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

        public void Test()
        {
            MessageBoxResult result = MessageBox.Show("Test");
        }


    }

}
