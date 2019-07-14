using Caliburn.Micro;
using RMDesktopUI.EventModels;
using RMDesktopUI.Library.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using ZXing.Aztec;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Controls;
using System.Timers;

namespace RMDesktopUI.ViewModels
{
    public class ScannerViewModel : Screen
    {
        internal readonly Services.IBarcodeScannerService BarcodeScannerService;

        private readonly IEventAggregator _eventAggregator;
        private readonly IProductEndpoint _productEndpoint;

        private INavigationEventWithParameters _navigateTo;

        public ScannerViewModel(
            IEventAggregator eventAggregator, 
            IProductEndpoint productEndpoint, 
            // TODO Resolve this service in view maybe, not sure, investigate???
            Services.IBarcodeScannerService barcodeScannerService)
        {
            _eventAggregator = eventAggregator;
            _productEndpoint = productEndpoint;

            BarcodeScannerService = barcodeScannerService;
        }

        public void SetNavigateToAfterResultScan(INavigationEventWithParameters navigationEventWithParameters)
        {
            _navigateTo = navigationEventWithParameters;
        }

        public void NavigateToView()
        {
            _eventAggregator.PublishOnUIThread(_navigateTo);
        }

        public void OnBarcodeResult(string decoded)
        {
            _navigateTo.Parameters = decoded;
            _eventAggregator.PublishOnUIThread(_navigateTo);
        }
    }
}
