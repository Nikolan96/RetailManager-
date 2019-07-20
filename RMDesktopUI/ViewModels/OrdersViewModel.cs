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
    public class OrdersViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IOrderEndpoint _orderEndpoint;
        private readonly ILoggedInUserModel _loggedInUserModel;
        private readonly IOrderItemEndpoint _orderItemEndpoint;
        private readonly IProductEndpoint _productEndpoint;

        public OrdersViewModel(IEventAggregator eventAggregator, IOrderEndpoint orderEndpoint, 
            ILoggedInUserModel loggedInUserModel, IOrderItemEndpoint orderItemEndpoint, IProductEndpoint productEndpoint)
        {
            _eventAggregator = eventAggregator;
            _orderEndpoint = orderEndpoint;
            _loggedInUserModel = loggedInUserModel;
            _orderItemEndpoint = orderItemEndpoint;
            _productEndpoint = productEndpoint;
        }

        private List<OrderModel> _allOrders;

        public List<OrderModel> AllOrders
        {
            get
            {
                return _allOrders;
            }
            set
            {
                _allOrders = value;

                NotifyOfPropertyChange(() => AllOrders);
                NotifyOfPropertyChange(() => IsApproved);
            }
        }

        private BindingList<OrderModel> _orders;

        public BindingList<OrderModel> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;

                NotifyOfPropertyChange(() => IsApproved);
                NotifyOfPropertyChange(() => Orders);
            }
        }

        private OrderModel _selectedOrder;

        public OrderModel SelectedOrder
        {
            get { return _selectedOrder; }
            set
            {
                _selectedOrder = value;

                NotifyOfPropertyChange(() => SelectedOrder);
                NotifyOfPropertyChange(() => CanDetails);
                NotifyOfPropertyChange(() => CanApprove);
                NotifyOfPropertyChange(() => CanDisapprove);
            }
        }

        private bool _isApproved = false;

        public bool IsApproved
        {
            get { return _isApproved; }
            set
            {
                _isApproved = value;

                if (value == false)
                {
                    _orders = new BindingList<OrderModel>(AllOrders.Where(t => t.IsApproved == false).ToList<OrderModel>());
                }
                else
                {
                    _orders = new BindingList<OrderModel>(AllOrders.Where(t => t.IsApproved == true).ToList<OrderModel>());
                }

                if (IsApproved == false && _loggedInUserModel.Role == "Manager")
                {
                    _isManagerAndApproved = "Visible";
                }
                else
                {
                    _isManagerAndApproved = "Hidden";
                }

                NotifyOfPropertyChange(() => IsApproved);
                NotifyOfPropertyChange(() => Orders);
                NotifyOfPropertyChange(() => IsManagerAndApproved);
            }
        }

        private string _isManagerAndApproved = "Visible";

        public string IsManagerAndApproved
        {
            get { return _isManagerAndApproved; }
            set
            {
                _isManagerAndApproved = value;

                NotifyOfPropertyChange(() => IsApproved);
                NotifyOfPropertyChange(() => IsManagerAndApproved);
            }
        }

        public bool CanDetails
        {
            get
            {
                bool output = false;

                if (SelectedOrder != null)
                {
                    output = true;
                }

                return output;
            }
        }

        public void Details()
        {
            _eventAggregator.PublishOnUIThread(new OrderDetailsViewEvent(SelectedOrder.ID, SelectedOrder.IsApproved));
        }

        public bool CanApprove
        {
            get
            {
                bool output = false;

                if (SelectedOrder != null)
                {
                    output = true;
                }

                return output;
            }
        }

        public async void Approve()
        {
            var orderToApprove = Orders.Where(s => s.ID == SelectedOrder.ID).First();

            List<OrderItemModel> orderItems = await _orderItemEndpoint.GetOrderItems(orderToApprove.ID);

            foreach (var product in orderItems)
            {
                UpdateProductQuantityModel quantityModel = new UpdateProductQuantityModel
                {
                    ID = product.ProductID,
                    QuantitySold = product.Quantity
                };

                await _productEndpoint.UpdateProductQuantityCanceled(quantityModel);
            }
            
            await _orderEndpoint.ApproveOrder(SelectedOrder.ID);

            await LoadOrders();
        }

        public bool CanDisapprove
        {
            get
            {
                bool output = false;

                if (SelectedOrder != null)
                {
                    output = true;
                }

                return output;
            }
        }

        public async void Disapprove()
        {
            await _orderItemEndpoint.DeleteOrderItems(SelectedOrder.ID);

            await _orderEndpoint.DeleteOrder(SelectedOrder.ID);

            await LoadOrders();
        }

        private async Task LoadOrders()
        {
            var orders = await _orderEndpoint.GetOrdersByShopID(_loggedInUserModel.ShopId);
            AllOrders = new List<OrderModel>(orders);
            IsApproved = false;
            Orders = new BindingList<OrderModel>(AllOrders.Where(t => t.IsApproved == false).ToList<OrderModel>());
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadOrders();
        }

        public void GoToProductsView()
        {
            _eventAggregator.PublishOnUIThread(new ProductsViewEvent());
        }
    }
}
