using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantClientService.Interaction
{
    public interface IBoolQuestion
    {
        Action<bool> BoolCallbackAction { get; set; }
        string Question { get; set; }
    }
}
