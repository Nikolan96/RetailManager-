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
    public class BillItemsViewModel : Screen
    {
        private readonly IAPIHelper _apiHelper;
        private readonly IEventAggregator _events;
        private readonly IBillItemEndpoint _billItemEndpoint;
        private string _billId { get; set; }

        public BillItemsViewModel(IAPIHelper apiHelper, IEventAggregator eventAggregator, IBillItemEndpoint billItemEndpoint)
        {
            _apiHelper = apiHelper;
            _events = eventAggregator;
            _billItemEndpoint = billItemEndpoint;
        }

        private BindingList<BillItemModel> _billItems = new BindingList<BillItemModel>();

        public BindingList<BillItemModel> BillItems
        {
            get { return _billItems; }
            set
            {
                _billItems = value;
                NotifyOfPropertyChange(() => BillItems);
            }
        }

        public void AddBillId(string BillId)
        {
            _billId = BillId;
        }

        public void GoToBillsView()
        {
            _events.PublishOnUIThread(new BillsViewEvent());
        }

        private async Task LoadBillItems()
        {
            try
            {
                var billItems = await _billItemEndpoint.GetBillItems(_billId);
                BillItems = new BindingList<BillItemModel>(billItems);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadBillItems();
        }
    }
}
