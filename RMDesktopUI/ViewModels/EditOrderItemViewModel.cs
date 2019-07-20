using Caliburn.Micro;
using RMDesktopUI.EventModels;
using RMDesktopUI.Library.Api;
using RMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
    public class EditOrderItemViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IOrderItemEndpoint _orderItemEndpoint;
        private readonly IOrderEndpoint _orderEndpoint;

        private int _id { get; set; }

        public EditOrderItemViewModel(IEventAggregator eventAggregator, IOrderItemEndpoint orderItemEndpoint, IOrderEndpoint orderEndpoint)
        {
            _eventAggregator = eventAggregator;
            _orderItemEndpoint = orderItemEndpoint;
            _orderEndpoint = orderEndpoint;
        }

        public void AddID(int ID)
        {
            _id = ID;
        }

        private string _productName;

        public string ProductName
        {
            get { return _productName; }
            set
            {
                _productName = value;
                NotifyOfPropertyChange(() => ProductName);
            }
        }

        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                NotifyOfPropertyChange(() => Quantity);
            } 
        }

        private string _orderID;

        public string OrderID
        {
            get { return _orderID; }
            set
            {
                _orderID = value;
                NotifyOfPropertyChange(() => OrderID);
            }
        }

        private bool _isApproved;

        public bool IsApproved
        {
            get { return _isApproved; }
            set
            {
                _isApproved = value;
                NotifyOfPropertyChange(() => IsApproved);
            }
        }

        private async Task LoadTextBoxes()
        {
            var orderItem = await _orderItemEndpoint.GetOrderItem(_id);
            ProductName = orderItem.ProductName;
            Quantity = orderItem.Quantity;
            OrderID = orderItem.OrderID;
            var order = await _orderEndpoint.GetOrderByID(OrderID);
            IsApproved = order.IsApproved;
        }


        public bool CanEditEdit
        {
            get
            {
                bool output = false;

                if (Quantity > 0)
                {
                    output = true;
                }

                return output;
            }
        }

        public async void Edit()
        {
            UpdateOrderItemModel updateOrderItem = new UpdateOrderItemModel
            {
                ID = _id,
                Quantity = Quantity
            };

            await _orderItemEndpoint.UpdateOrderItem(updateOrderItem);

            _eventAggregator.PublishOnUIThread(new OrderDetailsViewEvent(OrderID, IsApproved));
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadTextBoxes();
        }

        public void Back()
        {
            _eventAggregator.PublishOnUIThread(new OrderDetailsViewEvent(OrderID,IsApproved));
        }
    }
}
