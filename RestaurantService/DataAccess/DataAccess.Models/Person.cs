using System;
using System.Collections.Generic;

namespace DataAccess.DataAccess.Models
{
    public partial class Person
    {
        public Person()
        {
            Customer = new HashSet<Customer>();
            Employee = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int LocationId { get; set; }

        public virtual Location Location { get; set; }
        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
