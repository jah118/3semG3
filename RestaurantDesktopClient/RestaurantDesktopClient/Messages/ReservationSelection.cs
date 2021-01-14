using GalaSoft.MvvmLight.Messaging;

namespace RestaurantDesktopClient.Messages
{
    //kommnunikation imellem veiwmodels. Bruges for at have lav kopling
    public class ReservationSelection : MessageBase
    {
        public int Selected { get; set; }
    }
}
