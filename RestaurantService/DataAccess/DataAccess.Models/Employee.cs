using System;
using System.Collections.Generic;

namespace DataAccess.DataAccess.Models
{
    public partial class Employee
    {
        public Employee()
        {
            RestaurantOrder = new HashSet<RestaurantOrder>();
        }

        public int Id { get; set; }
        public int? PersonId { get; set; }
        public int TitleId { get; set; }
        public decimal? Salary { get; set; }

        public virtual Person Person { get; set; }
        public virtual EmployeeTitle Title { get; set; }
        public virtual ICollection<RestaurantOrder> RestaurantOrder { get; set; }
    }
}
