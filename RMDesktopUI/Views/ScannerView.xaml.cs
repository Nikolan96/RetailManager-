using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing.Imaging;
using System.Timers;
using ZXing;
using System.Threading;
using RMDesktopUI.ViewModels;

namespace RMDesktopUI.Views
{
    /// <summary>
    /// Interaction logic for ScannerView.xaml
    /// </summary>
    public partial class ScannerView : UserControl
    {
        private ScannerViewModel _scannerViewModel => DataContext as ScannerViewModel;

        public ScannerView()
        {
            InitializeComponent();
            
            Loaded += ScannerView_Loaded;
            Unloaded += ScannerView_Unloaded;         
        }

        private void ScannerView_Loaded(object sender, RoutedEventArgs e)
        {
            _scannerViewModel.BarcodeScannerService.OnResult += BarcodeScannerService_OnResult;
            _scannerViewModel.BarcodeScannerService.OnNewFrame += BarcodeScannerService_OnNewFrame;

            _scannerViewModel.BarcodeScannerService.StartScan();
        }

        private void BarcodeScannerService_OnNewFrame(object sender, System.Windows.Media.ImageSource imageSource)
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                frameHolder.Source = imageSource;
            }));
        }

        private void BarcodeScannerService_OnResult(object sender, string result)
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                _scannerViewModel.OnBarcodeResult(result);
            }));
        }

        private void ScannerView_Unloaded(object sender, RoutedEventArgs e)
        {
            _scannerViewModel.BarcodeScannerService.StopScan();

            _scannerViewModel.BarcodeScannerService.OnResult -= BarcodeScannerService_OnResult;
            _scannerViewModel.BarcodeScannerService.OnNewFrame -= BarcodeScannerService_OnNewFrame;
        }
    }
}
