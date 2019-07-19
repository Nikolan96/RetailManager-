﻿using Caliburn.Micro;
using RMDesktopUI.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
    public class OrdersViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;

        public OrdersViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void GoToProductsView()
        {
            _eventAggregator.PublishOnUIThread(new ProductsViewEvent());
        }

        public void Details()
        {
            _eventAggregator.PublishOnUIThread(new OrderDetailsViewEvent());
        }
    }
}
