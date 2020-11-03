using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Location
    {
        public Location()
        {
            Person = new HashSet<Person>();
        }

        public int Id { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }

        public virtual ZipId ZipCodeNavigation { get; set; }
        public virtual ICollection<Person> Person { get; set; }
    }
}
