using Caliburn.Micro;
using RMDesktopUI.EventModels;
using RMDesktopUI.Library.Api;
using RMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RMDesktopUI.ViewModels
{
    public class EditProductViewModel : Screen
    {

        private IAPIHelper _apiHelper;
        private IEventAggregator _events;
        private readonly IProductEndpoint _productEndpoint;
        private ProductModel _productModel;

        public EditProductViewModel(IAPIHelper apiHelper, IEventAggregator events, IProductEndpoint productEndpoint)
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
                NotifyOfPropertyChange(() => CanEdit);
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
                NotifyOfPropertyChange(() => CanEdit);
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
                NotifyOfPropertyChange(() => CanEdit);
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
                NotifyOfPropertyChange(() => CanEdit);
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
                NotifyOfPropertyChange(() => CanEdit);
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
                NotifyOfPropertyChange(() => CanEdit);
                NotifyOfPropertyChange(() => TaxTb);
            }
        }

        private int _quantityTb;

        public int QuantityTb
        {
            get { return _quantityTb; }
            set
            {
                _quantityTb = value;
                NotifyOfPropertyChange(() => CanEdit);
                NotifyOfPropertyChange(() => QuantityTb);
            }
        }

        public bool CanEdit
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

        public void FillTextboxes()
        {
            ProductNameTb = _productModel.ProductName;
            CategoryTb = _productModel.Category;
            DescriptionTb = _productModel.Description;
            PurchasePriceTb = _productModel.PurchasePrice;
            RetailPriceTb = _productModel.RetailPrice;
            TaxTb = _productModel.Tax;
            QuantityTb = _productModel.QuantityInStock;
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            FillTextboxes();
        }

        public void Back()
        {
            _events.PublishOnUIThread(new ProductsViewEvent());
        }

        public async Task Edit()
        {
            UpdateProductModel updateProductModel = new UpdateProductModel
            {
                Id = _productModel.Id,
                ProductName = ProductNameTb,
                Category = CategoryTb,
                Description = DescriptionTb,
                PurchasePrice = PurchasePriceTb,
                RetailPrice = RetailPriceTb,
                Tax = TaxTb,
                QuantityInStock = QuantityTb              
            };
            await _productEndpoint.UpdateProduct(updateProductModel);
            _events.PublishOnUIThread(new ProductsViewEvent());
        }

        public void AddProductModel(ProductModel productModel)
        {
            _productModel = productModel;
        }
    }
}
