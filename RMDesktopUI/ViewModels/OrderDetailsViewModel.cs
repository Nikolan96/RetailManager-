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
    public class OrderDetailsViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IOrderItemEndpoint _orderItemEndpoint;

        private string _orderID { get; set; }

        private bool _isApproved;

        public bool IsApproved
        {
            get { return _isApproved; }
            set
            {
                _isApproved = value;

                if (value == true)
                {
                    OrderIsApproved = "Hidden";
                }
                else
                {
                    OrderIsApproved = "Visible";
                }

                NotifyOfPropertyChange(() => IsApproved);
                NotifyOfPropertyChange(() => OrderIsApproved);
            }
        }

        public OrderDetailsViewModel(IEventAggregator eventAggregator, IOrderItemEndpoint orderItemEndpoint)
        {
            _eventAggregator = eventAggregator;
            _orderItemEndpoint = orderItemEndpoint;
        }

        private BindingList<OrderItemModel> _orderItems;

        public BindingList<OrderItemModel> OrderItems
        {
            get { return _orderItems; }
            set
            {
                _orderItems = value;
                NotifyOfPropertyChange(() => OrderItems);
            }
        }

        private OrderItemModel _selectedOrderItem;

        public OrderItemModel SelectedOrderItem
        {
            get { return _selectedOrderItem; }
            set
            {
                _selectedOrderItem = value;
                NotifyOfPropertyChange(() => SelectedOrderItem);
                NotifyOfPropertyChange(() => CanEditOrderItem);
                NotifyOfPropertyChange(() => CanDeleteOrderItem);
            }
        }

        private string _orderIsApproved;

        public string OrderIsApproved
        {
            get { return _orderIsApproved; }
            set
            {
                _orderIsApproved = value;
                NotifyOfPropertyChange(() => SelectedOrderItem);
            }
        }

        public void AddOrderID(string OrderID)
        {
            _orderID = OrderID;
        }

        public void AddIsApproved(bool IsApproved)
        {
            this.IsApproved = IsApproved;
        }

        private async Task LoadOrderItems()
        {
            var orderItems = await _orderItemEndpoint.GetOrderItems(_orderID);
            OrderItems = new BindingList<OrderItemModel>(orderItems);
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadOrderItems();
        }

        public bool CanEditOrderItem
        {
            get
            {
                bool output = false;

                if (SelectedOrderItem != null)
                {
                    output = true;
                }

                return output;
            }
        }

        public void EditOrderItem()
        {
            _eventAggregator.PublishOnUIThread(new EditOrderItemViewEvent(SelectedOrderItem.ID));
        }

        public bool CanDeleteOrderItem
        {
            get
            {
                bool output = false;

                if (SelectedOrderItem != null)
                {
                    output = true;
                }

                return output;
            }
        }

        public async void DeleteOrderItem()
        {
            await _orderItemEndpoint.DeleteOrderItem(SelectedOrderItem.ID);
            await LoadOrderItems();
        }

        public void Back()
        {
            _eventAggregator.PublishOnUIThread(new OrdersViewEvent());
        }
    }
}
