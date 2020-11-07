using DataAccess.DataTransferObjects;
using RestaurantDesktopClient.Services.CustomerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDesktopClient.Controllers
{
    class CustomerController
    {
        internal CustomerDTO getCustomerByid(int customerId)
        {
            ICustomerRepository ic = new CustomerRepository();
            CustomerDTO res = ic.GetCustomerById(customerId);
            return res;

        }
    }
}
