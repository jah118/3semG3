using System;
using System.Collections.Generic;

namespace DataTransferObjects
{
    public partial class Price
    {
        public int Id { get; }
        public decimal PriceValue { get;  }
        public FoodDTO Food { get;  }
    }
}
