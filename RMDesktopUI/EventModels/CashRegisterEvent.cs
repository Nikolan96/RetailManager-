namespace RMDesktopUI.EventModels
{
    public class CashRegisterEvent : INavigationEvent
    {

    }

    public class CashRegisterEventWithScanResult : INavigationEventWithParameters
    {
        public object Parameters { get; set; }
    }

}
