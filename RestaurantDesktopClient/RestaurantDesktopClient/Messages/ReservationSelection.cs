using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;

namespace RestaurantDesktopClient.Messages
{
    public class ReservationSelection : MessageBase
    {
        public int Selected { get; set; }
    }
}
