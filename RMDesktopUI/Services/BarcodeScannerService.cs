using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ZXing;

namespace RMDesktopUI.Services
{
    public class BarcodeScannerService : IBarcodeScannerService
    {
        private VideoCaptureDevice _videoCaptureDevice;
        private FilterInfoCollection _filterInfoCollection;
        private DateTime _nextBarcodeProcessingTime;
        private BarcodeReader _barcodeReader;

        public BarcodeScannerService()
        {
            _filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            _videoCaptureDevice = new VideoCaptureDevice(_filterInfoCollection[0].MonikerString);
            _videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            _barcodeReader = new BarcodeReader();
        }

        public event EventHandler<string> OnResult;

        public event EventHandler<ImageSource> OnNewFrame;

        public void StartScan()
        {
            _videoCaptureDevice.Start();
        }

        public void StopScan()
        {
            _videoCaptureDevice.Stop();
        }

        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                // Get the image from frame
                var frameImage = (Bitmap)eventArgs.Frame.Clone();
                var imageSouce = new BitmapImage();

                // TODO Dispose memory stream
                var ms = new MemoryStream();
                frameImage.Save(ms, ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                imageSouce.BeginInit();
                imageSouce.StreamSource = ms;
                imageSouce.EndInit();
                imageSouce.Freeze();

                OnNewFrame?.Invoke(this, imageSouce);

                // Process the barcode
                var currentTime = DateTime.Now;

                if (currentTime >= _nextBarcodeProcessingTime)
                {
                    var result = _barcodeReader.Decode(frameImage);
                    var decoded = result?.Text.Trim();
                    if (!string.IsNullOrEmpty(decoded))
                    {
                        OnResult?.Invoke(this, decoded);
                    }

                    _nextBarcodeProcessingTime = currentTime.AddMilliseconds(100);
                }
            }
            catch (Exception ex)
            {
                // ignored
            }
        }
    }
}
