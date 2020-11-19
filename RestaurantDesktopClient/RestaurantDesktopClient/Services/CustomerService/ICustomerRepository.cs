using DataAccess.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDesktopClient.Services.CustomerService
{
    interface ICustomerRepository
    {
        CustomerDTO GetCustomerById(int customerId);
    }
}
