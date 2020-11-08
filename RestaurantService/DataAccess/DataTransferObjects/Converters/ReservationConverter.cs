using DataAccess.Models;
using System;

namespace DataAccess.DataTransferObjects.Converters
{
    public partial class Converter
    {
        //Unsure if this makes more sense than disretely handling dto -> Model mapping, and having a secondary constructor
        public static Reservation Convert(ReservationDTO obj)
        {
            throw new NotImplementedException();
        }

        public static ReservationDTO Convert(Reservation obj)
        {
            throw new NotImplementedException();
        }
    }
}