using System;

namespace DataAccess.DataTransferObjects
{
    public class PriceDTO
    {
        public PriceDTO()
        {
            
        }
        public PriceDTO(double price)
        {
            PriceValue = Convert.ToDecimal(price);
        }
        // DO NOT USE UNTIL WE FIGURE OUT IT's NEEDED
        public int Id { get; init; }

        public decimal PriceValue { get; set; }
        
    }
}