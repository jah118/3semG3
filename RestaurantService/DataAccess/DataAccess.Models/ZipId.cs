using System;
using System.Collections.Generic;

namespace DataAccess.DataAccess.Models
{
    public partial class ZipId
    {
        public ZipId()
        {
            Location = new HashSet<Location>();
        }

        public string ZipCode { get; set; }
        public string City { get; set; }

        public virtual ICollection<Location> Location { get; set; }
    }
}
