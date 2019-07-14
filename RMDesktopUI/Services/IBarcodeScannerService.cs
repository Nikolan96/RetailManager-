using System;
using System.Windows.Media;

namespace RMDesktopUI.Services
{
    public interface IBarcodeScannerService
    {
        event EventHandler<string> OnResult;

        event EventHandler<ImageSource> OnNewFrame;

        void StartScan();

        void StopScan();
    }
}
