using Caliburn.Micro;
using RMDesktopUI.EventModels;
using RMDesktopUI.Library.Api;
using RMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.IO;
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
        private readonly IBillEndpoint _billEndpoint;
        private readonly IBillItemEndpoint _billItemEndpoint;
        private readonly ILoggedInUserModel _loggedInUser;

        public CashRegisterViewModel(IAPIHelper apiHelper, IEventAggregator events, IProductEndpoint productEndpoint, 
            IBillEndpoint billEndpoint, IBillItemEndpoint billItemEndpoint, ILoggedInUserModel loggedInUser)
        {
            _apiHelper = apiHelper;
            _events = events;
            _productEndpoint = productEndpoint;
            _billEndpoint = billEndpoint;
            _billItemEndpoint = billItemEndpoint;
            _loggedInUser = loggedInUser;
        }

        public string User
        {
            get { return _loggedInUser.FirstName; }
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

                if (Paid > Total && Total > 0)
                {
                    NotifyOfPropertyChange(() => Change);
                    NotifyOfPropertyChange(() => DisplayChange);
                }
                
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
                if (Paid > 0)
                {
                    return Paid - Total;
                }

                return 0;
                
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
                if (Paid > 0)
                {
                    var value = Decimal.Round(Change, 2);
                    return value.ToString();
                }

                return "0";
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
                if (SearchBy == true)
                {
                    _selectedProductName = value;
                }
               
                NotifyOfPropertyChange(() => SelectedProductName);
                NotifyOfPropertyChange(() => CanAdd);
                NotifyOfPropertyChange(() => SearchBy);
            }
        }

        private string _selectedProductId;

        public string SelectedProductId
        {
            get { return _selectedProductId; }
            set
            {
                _selectedProductId = value;
                NotifyOfPropertyChange(() => SelectedProductId);
                NotifyOfPropertyChange(() => CanAdd);
            }
        }

        private bool _searchBy = true;

        public bool SearchBy
        {
            get { return _searchBy; }
            set
            {
                if (value == true)
                {
                    ID = "";
                    SearchByName = "Visible";
                    SearchByID = "Hidden";
                }
                else
                {

                    SelectedProductName = null;
                    SearchByName = "Hidden";
                    SearchByID = "Visible";
                }

                _searchBy = value;
                NotifyOfPropertyChange(() => SearchBy);
                NotifyOfPropertyChange(() => SearchByName);
                NotifyOfPropertyChange(() => SearchByID);
            }
        }


        private string _searchByName = "Visible";

        public string SearchByName
        {
            get { return _searchByName; }
            set
            {
                _searchByName = value;
                NotifyOfPropertyChange(() => SearchByName);
            }
        }

        private string _searchByID = "Hidden";

        public string SearchByID
        {
            get { return _searchByID; }
            set
            {
                _searchByID = value;
                NotifyOfPropertyChange(() => SearchByID);
            }
        }

        private string _iD;

        public string ID
        {
            get { return _iD; }
            set
            {
                _iD = value;
                NotifyOfPropertyChange(() => ID);
                NotifyOfPropertyChange(() => CanAdd);
            }
        }


        public bool CanAdd
        {
            get
            {
                bool output = false;


                if (SearchBy == true)
                {
                    if (SelectedProductName != null && Quantity > 0)
                    {
                        output = true;
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(ID) && Quantity > 0)
                    {
                        output = true;
                    }
                }

                return output;
            }           
        }

        public async void Add()
        {

            ProductModel ProductModel = new ProductModel();
            ProductModel existing;

            if (SearchBy == true)
            {
                ProductModel = await _productEndpoint.GetByProductName(SelectedProductName);
                existing = Products.FirstOrDefault(x => x.ProductName == SelectedProductName);
            }
            else
            {
                ProductModel = await _productEndpoint.GetProductByID(ID);
                existing = Products.FirstOrDefault(x => x.ID == ID);
            }

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
                    ID = ProductModel.ID,
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
            ID = null;
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
            Total -= SelectedBillItem.RetailPrice * SelectedBillItem.QuantityInStock;
            Products.Remove(SelectedBillItem);
            SelectedBillItem = null;

            Paid = 0;

            NotifyOfPropertyChange(() => Paid);
            NotifyOfPropertyChange(() => CanDeleteBill);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => DisplayTotal);
            NotifyOfPropertyChange(() => CanCash);
            NotifyOfPropertyChange(() => Products);
            NotifyOfPropertyChange(() => Change);
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
            foreach (var item in Products)
            {
                Total -= item.RetailPrice * item.QuantityInStock;
            }

            Paid = 0;
           
            Products.Clear();

            NotifyOfPropertyChange(() => Paid);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => DisplayTotal);
            NotifyOfPropertyChange(() => CanDeleteBill);
            NotifyOfPropertyChange(() => CanCash);
            NotifyOfPropertyChange(() => DisplayChange);
            NotifyOfPropertyChange(() => Change);
            NotifyOfPropertyChange(() => Products);
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

        public async Task Cash()
        {

            InsertBillModel bill = new InsertBillModel
            {
                ShopId = _loggedInUser.ShopId,
                Total = Total,
                Paid = Paid,
                Change = Change,
                UserId = _loggedInUser.ID,
                CreatedDate = DateTime.Now,
                Id = Guid.NewGuid().ToString()
      
             };

            await _billEndpoint.InsertBill(bill);

            foreach (var item in Products)
            {
                InsertBillItemModel billItem = new InsertBillItemModel
                {
                    ProductName = item.ProductName,
                    Category = item.Category,
                    Description = item.Description,
                    RetailPrice = item.RetailPrice,
                    Quantity = item.QuantityInStock,
                    BillId = bill.Id,
                    ProductID = item.ID
                    
                };

                await _billItemEndpoint.InsertBillItem(billItem);

                UpdateProductQuantityModel quantityModel = new UpdateProductQuantityModel()
                {
                    ID = item.ID,
                    QuantitySold = item.QuantityInStock
                };

                await _productEndpoint.UpdateProductQuantitySold(quantityModel);
            }

            string items = "";

            foreach (var item in Products)
            {
                items += item.ProductName + "\n";
            }

            items += "Paid:" + Paid + "\n";
            items += "Total:" + DisplayTotal + "\n";
            items += "Change:" + DisplayChange + "\n";

            MessageBox.Show(items);

            DeleteBill();
        }

        private async Task LoadProductNames()
        {
            var productNames = await _productEndpoint.GetAllProductNames(_loggedInUser.ShopId);
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

        public void GoToBillsView()
        {
            _events.PublishOnUIThread(new BillsViewEvent());
        }

        public void Back()
        {
                _events.PublishOnUIThread(new LogoutEvent());
        }

        public void GoToScanner()
        {
            _events.BeginPublishOnUIThread(new ScannerViewEvent(new CashRegisterEventWithScanResult()));
        }
    }
}
