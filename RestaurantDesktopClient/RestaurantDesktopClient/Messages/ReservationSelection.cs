using GalaSoft.MvvmLight.Messaging;

namespace RestaurantDesktopClient.Messages
{
    public class ReservationSelection : MessageBase
    {
        public int Selected { get; set; }
    }
}
