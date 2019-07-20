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
    public class OrderFormViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IProductEndpoint _productEndpoint;
        private readonly ILoggedInUserModel _loggedInUser;
        private readonly IOrderEndpoint _orderEndpoint;
        private readonly IOrderItemEndpoint _orderItemEndpoint;

        public OrderFormViewModel(IEventAggregator eventAggregator, IProductEndpoint productEndpoint,
            ILoggedInUserModel loggedInUser, IOrderEndpoint orderEndpoint, IOrderItemEndpoint orderItemEndpoint)
        {
            _eventAggregator = eventAggregator;
            _productEndpoint = productEndpoint;
            _loggedInUser = loggedInUser;
            _orderEndpoint = orderEndpoint;
            _orderItemEndpoint = orderItemEndpoint;
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

        private BindingList<OrderItemModel> _orderItemsToAdd = new BindingList<OrderItemModel>();

        public BindingList<OrderItemModel> OrderItemsToAdd
        {
            get { return _orderItemsToAdd; }
            set
            {
                _orderItemsToAdd = value;
                NotifyOfPropertyChange(() => OrderItemsToAdd);
            }
        }

        private OrderItemModel _selectedOrderItemToAdd;

        public OrderItemModel SelectedOrderItemToAdd
        {
            get { return _selectedOrderItemToAdd; }
            set
            {
                _selectedOrderItemToAdd = value;
                NotifyOfPropertyChange(() => SelectedOrderItemToAdd);
                NotifyOfPropertyChange(() => CanCancelOrderItem);
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

                if (SelectedProductName != null && QuantityTb != 0)
                {
                    output = true;
                }

                return output;
            }
        }

        public async void AddOrder()
        {
            ProductModel product = await _productEndpoint.GetByProductName(SelectedProductName);
            OrderItemModel existingOrderItem;

            existingOrderItem = OrderItemsToAdd.Where(n => n.ProductName == SelectedProductName).FirstOrDefault();

            if (existingOrderItem != null)
            {
                existingOrderItem.Quantity += QuantityTb;
                OrderItemsToAdd.Remove(existingOrderItem);
                OrderItemsToAdd.Add(existingOrderItem);
            }
            else
            {
                OrderItemModel orderItem = new OrderItemModel
                {
                    ProductName = SelectedProductName,
                    Quantity = QuantityTb,
                    ProductID = product.ID                   
                };

                OrderItemsToAdd.Add(orderItem);
            }

            QuantityTb = 0;
            SelectedProductName = null;
            CurrentQuantityTb = 0;

            NotifyOfPropertyChange(() => CurrentQuantityTb);
            NotifyOfPropertyChange(() => SelectedProductName);
            NotifyOfPropertyChange(() => QuantityTb);
            NotifyOfPropertyChange(() => OrderItemsToAdd);
            NotifyOfPropertyChange(() => CanOrder);
        }

        public bool CanCancelOrderItem
        {
            get
            {
                bool output = false;

                if (SelectedOrderItemToAdd != null)
                {
                    output = true;
                }

                return output;
            }
        }

        public void CancelOrderItem()
        {
            OrderItemsToAdd.Remove(SelectedOrderItemToAdd);

            NotifyOfPropertyChange(() => OrderItemsToAdd);
        }

        public bool CanOrder
        {
            get
            {
                bool output = false;

                if (OrderItemsToAdd.Count != 0)
                {
                    output = true;
                }

                return output;
            }
        }

        public async void Order()
        {
            InsertOrderModel order = new InsertOrderModel
            {
                ID = Guid.NewGuid().ToString(),
                ShopID = _loggedInUser.ShopId,
                CreatedDate = DateTime.Now
            };
    
            await _orderEndpoint.InsertOrder(order);

            foreach (var orderitem in OrderItemsToAdd)
            {
                InsertOrderItemModel insertOrderItemModel = new InsertOrderItemModel
                {
                    OrderID = order.ID,
                    ProductName = orderitem.ProductName,
                    Quantity = orderitem.Quantity,
                    ProductID = orderitem.ProductID
                };

                await _orderItemEndpoint.InsertOrderItem(insertOrderItemModel);
            }

            MessageBox.Show("Created Order Successfully");

            OrderItemsToAdd.Clear();
            NotifyOfPropertyChange(() => OrderItemsToAdd);

            _eventAggregator.PublishOnUIThread(new ProductsViewEvent());
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
