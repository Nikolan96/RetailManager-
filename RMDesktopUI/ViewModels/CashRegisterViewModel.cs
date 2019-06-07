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

        private int _quantity = 1;

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                NotifyOfPropertyChange(() => Quantity);
            }
        }

        private decimal _paid;

        public decimal Paid
        {
            get { return _paid; }
            set
            {
                _paid = value;
                NotifyOfPropertyChange(() => Paid);
                NotifyOfPropertyChange(() => Change);
                NotifyOfPropertyChange(() => DisplayChange);
                NotifyOfPropertyChange(() => CanCash);
            }
        }

        private decimal _total = 0;

        public decimal Total
        {
            get
            {
                return _total;
            }
            set
            {
                _total = value;
                NotifyOfPropertyChange(() => Total);
            }
        }

        public string DisplayTotal
        {
            get
            {
                var value = Decimal.Round(Total, 2);
                return value.ToString();
            }
        }


        private decimal _change;

        public decimal Change
        {
            get
            {
                return Paid - Total;
            }
            set
            {          
                _change = Paid - Total;
                NotifyOfPropertyChange(() => Change);
            }
        }


        public string DisplayChange
        {
            get
            {
                var value = Decimal.Round(Change, 2);
                return value.ToString();
            }
        }

        private BindingList<ProductModel> _products = new BindingList<ProductModel>();

        public BindingList<ProductModel> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
                NotifyOfPropertyChange(() => CanDeleteBill);
                NotifyOfPropertyChange(() => Total);
                NotifyOfPropertyChange(() => DisplayTotal);
            }
        }

        private ProductModel _selectedBillItem;

        public ProductModel SelectedBillItem
        {
            get { return _selectedBillItem; }
            set
            {
                _selectedBillItem = value;
                NotifyOfPropertyChange(() => SelectedBillItem);
                NotifyOfPropertyChange(() => CanDeleteBillItem);
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

        private string _selectedProductName;

        public string SelectedProductName
        {
            get { return _selectedProductName; }
            set
            {
                _selectedProductName = value;
                NotifyOfPropertyChange(() => SelectedProductName);
                NotifyOfPropertyChange(() => CanAdd);
            }
        }

        private int _selectedProductId;

        public int SelectedProductId
        {
            get { return _selectedProductId; }
            set
            {
                _selectedProductId = value;
                NotifyOfPropertyChange(() => SelectedProductId);
                NotifyOfPropertyChange(() => CanAdd);
            }
        }

        public bool CanAdd
        {
            get
            {
                bool output = false;

                if (SelectedProductName != null || SelectedProductId > 0)
                {
                    output = true;
                }

                return output;
            }           
        }

        public async void Add()
        {
            
            ProductModel ProductModel = await _productEndpoint.GetByProductName(SelectedProductName);
            ProductModel existing = Products.FirstOrDefault(x => x.ProductName == SelectedProductName);

            if (existing != null)
            {
                existing.QuantityInStock += Quantity;
                Total += existing.RetailPrice * Quantity;
                Products.Remove(existing);
                Products.Add(existing);
            }
            else
            {
                // add automapper
                ProductModel productModelToAdd = new ProductModel
                {
                    Id = ProductModel.Id,
                    ProductName = ProductModel.ProductName,
                    Category = ProductModel.Category,
                    Description = ProductModel.Description,
                    PurchasePrice = ProductModel.PurchasePrice,
                    RetailPrice = ProductModel.RetailPrice,
                    Tax = ProductModel.Tax,
                    QuantityInStock = Quantity
                };

                Total += productModelToAdd.RetailPrice * Quantity;
                Products.Add(productModelToAdd);

            }

            Quantity = 1;
            SelectedProductName = null;
            NotifyOfPropertyChange(() => CanAdd);
            NotifyOfPropertyChange(() => CanDeleteBill);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => DisplayTotal);
            NotifyOfPropertyChange(() => CanCash);
            NotifyOfPropertyChange(() => DisplayChange);

        }


        public bool CanDeleteBillItem
        {
            get
            {
                bool output = false;

                if (SelectedBillItem != null)
                {
                    output = true;
                }

                return output;
            }
        }

        public void DeleteBillItem()
        {
            Products.Remove(SelectedBillItem);
            SelectedBillItem = null;
            NotifyOfPropertyChange(() => CanDeleteBill);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => DisplayTotal);
            NotifyOfPropertyChange(() => CanCash);
            NotifyOfPropertyChange(() => DisplayChange);
        }

        public bool CanDeleteBill
        {
            get
            {
                bool output = false;

                if (Products.Count > 0)
                {
                    output = true;
                }

                return output;
            }
        }

        public void DeleteBill()
        {
            Products.Clear();
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanDeleteBill);
            NotifyOfPropertyChange(() => CanCash);
            NotifyOfPropertyChange(() => DisplayChange);
        }

        public bool CanCash
        {
            get
            {
                bool output = false;

                if (Products.Count > 0 && Paid > 0 && Paid > Total)
                {
                    output = true;
                }

                return output;
            }
        }

        public void Cash()
        {
            string items = "";

            foreach (var item in Products)
            {
                items += item.ProductName + "\n";
            }

            items += "Paid:" + Paid + "\n";
            items += "Total:" + DisplayTotal + "\n";
            items += "Change:" + DisplayChange + "\n";

            MessageBox.Show(items);
        }

        private async Task LoadProductNames()
        {
            var productNames = await _productEndpoint.GetAllProductNames();
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
