using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataTransferObjects
{
    public class EmployeeDTO
    {

        public int Id { get; }
        public string Title { get; }
        public string Phone { get;  }
        public string Email { get;}
        public string FirstName { get; }
        public string LastName { get; }
        public string Address { get;  }
        public string ZipCode { get;  }
        public string City { get;  }

        public virtual ICollection<RestaurantOrderDTO> RestaurantOrder { get; set; }
    }
}
