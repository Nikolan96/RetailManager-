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
    public class BillsViewModel : Screen
    {
        private readonly IAPIHelper _apiHelper;
        private readonly IEventAggregator _events;
        private readonly IBillEndpoint _billEndpoint;
        private readonly IBillItemEndpoint _billItemEndpoint;
        private readonly ILoggedInUserModel _loggedInUser;

        public BillsViewModel(IAPIHelper apiHelper, IEventAggregator eventAggregator, IBillEndpoint billEndpoint, IBillItemEndpoint billItemEndpoint, ILoggedInUserModel loggedInUser)
        {
            _apiHelper = apiHelper;
            _events = eventAggregator;
            _billEndpoint = billEndpoint;
            _billItemEndpoint = billItemEndpoint;
            _loggedInUser = loggedInUser;
        }

        private BindingList<BillModel> _bills = new BindingList<BillModel>();

        public BindingList<BillModel> Bills
        {
            get { return _bills; }
            set
            {
                _bills = value;
                NotifyOfPropertyChange(() => Bills);
            }
        }

        private BillModel _selectedBill;

        public BillModel SelectedBill
        {
            get { return _selectedBill; }
            set
            {
                _selectedBill = value;
                NotifyOfPropertyChange(() => SelectedBill);
                NotifyOfPropertyChange(() => CanDelete);
                NotifyOfPropertyChange(() => CanGoToBillItemsView);
            }
        }

        public bool CanDelete
        {
            get
            {
                bool output = false;

                if (SelectedBill != null)
                {
                    output = true;
                }

                return output;
            }        
        }

        public async Task Delete()
        {
            await _billItemEndpoint.Delete(SelectedBill.Id);

            await _billEndpoint.Delete(SelectedBill.Id);

            Bills.Remove(SelectedBill);

            SelectedBill = null;
            NotifyOfPropertyChange(() => Bills);
        }

        public bool CanGoToBillItemsView
        {
            get
            {
                bool output = false;

                if (SelectedBill != null)
                {
                    output = true;
                }

                return output;
            }
        }

        private string _isManager;

        public string IsManager
        {
            get { return _isManager; }
            set
            {
                _isManager = value;
                NotifyOfPropertyChange(() => IsManager);
            }
        }

        public void BackToCashRegister()
        {
            _events.PublishOnUIThread(new CashRegisterEvent());
        }

        public void GoToBillItemsView()
        {
            _events.PublishOnUIThread(new BillItemsViewEvent(SelectedBill.Id));
        }

        public void ViewSelected()
        {
            _events.PublishOnUIThread(new CashRegisterEvent());
        }

        private async Task LoadBills()
        {
            var bills = await _billEndpoint.GetBills();
            Bills = new BindingList<BillModel>(bills);
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadBills();

            if (_loggedInUser.Role == "Manager")
            {
                IsManager = "Visible";
            }
            else
            {
                IsManager = "Hidden";
            }

            NotifyOfPropertyChange(() => IsManager);
        }
    }
}
