using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using RMDesktopUI.EventModels;

namespace RMDesktopUI.ViewModels
{
    // Conductor holds on to and activates only one item at a time.
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>, IHandle<CashRegisterEvent>, IHandle<ProductsViewEvent>, IHandle<EditProductViewEvent>
    {
        private readonly IEventAggregator _events;
        private readonly SalesViewModel _salesVM;
        private readonly SimpleContainer _container;
        private readonly ProductsViewModel _productsVM;
        private readonly EditProductViewModel _editProductViewModel;
        private readonly CashRegisterViewModel _cashRegisterVM;

        // Uses constructor injection to pass in a new instance of LoginVM and activate it.
        public ShellViewModel(IEventAggregator events, SalesViewModel salesVM, CashRegisterViewModel cashRegisterVM, SimpleContainer container,ProductsViewModel ProductsVM, EditProductViewModel editProductViewModel)
        {
            _events = events;
            _salesVM = salesVM;
            _cashRegisterVM = cashRegisterVM;
            _container = container;
            _productsVM = ProductsVM;
            _editProductViewModel = editProductViewModel;

            // Subscribes instance of shellview to events
            _events.Subscribe(this);

            ActivateItem(_container.GetInstance<LoginViewModel>()); // gets new instance of LoginViewModel and places it in _loginVM ( cleanes info from last one ).
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_cashRegisterVM);           
        }

        public void Handle(CashRegisterEvent messager)
        {
            ActivateItem(_cashRegisterVM);
        }

        public void Handle(ProductsViewEvent message)
        {
            ActivateItem(_productsVM);
        }

        public void Handle(EditProductViewEvent message)
        {
            _editProductViewModel.AddProductModel(message.SelectedProduct);
            ActivateItem(_editProductViewModel);
        }
    }
}
