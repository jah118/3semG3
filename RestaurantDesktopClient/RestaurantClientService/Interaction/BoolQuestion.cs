using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantClientService.Interaction
{
    public class BoolQuestion : IBoolQuestion
    {
        public Action<bool> BoolCallbackAction { get; set; }
        public string Question { get; set; }
    }
}
