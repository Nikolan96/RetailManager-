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

namespace RMDesktopUI.ViewModels
{
    public class ScannerViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IProductEndpoint _productEndpoint;

        private FilterInfoCollection CaptureDevice;
        private VideoCaptureDevice FinalFrame;

        public ScannerViewModel(IEventAggregator eventAggregator, IProductEndpoint productEndpoint)
        {
            _eventAggregator = eventAggregator;
            _productEndpoint = productEndpoint;
            Devices = new BindingList<string>();
        }

        private BindingList<string> _devices;

        public BindingList<string> Devices
        {
            get { return _devices; }
            set
            {
                _devices = value;
                NotifyOfPropertyChange(() => Devices);
            }
        }

        private int _selectedIndex;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                NotifyOfPropertyChange(() => SelectedIndex);
            }
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in CaptureDevice)
            {
                Devices.Add(device.Name);
            }

            FinalFrame = new VideoCaptureDevice();
        }

        public MediaElement ImageFeed { get; set; }

        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            ImageFeed.Source = (Uri)eventArgs.Frame.Clone();
        }

        public void OpenCamera()
        {

            FinalFrame = new VideoCaptureDevice(CaptureDevice[SelectedIndex].MonikerString);
            FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
            FinalFrame.Start();

        }

        public void GoToCashRegister()
        {
            _eventAggregator.PublishOnUIThread(new CashRegisterEvent());
        }
    }
}
