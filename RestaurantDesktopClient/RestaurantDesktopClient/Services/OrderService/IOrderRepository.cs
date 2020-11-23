using DataAccess.DataTransferObjects;
using System.Collections.Generic;

namespace RestaurantDesktopClient.Services.OrderService
{
    internal interface IOrderRepository
    {
        List<OrderDTO> GetAllOrders();
    }
}