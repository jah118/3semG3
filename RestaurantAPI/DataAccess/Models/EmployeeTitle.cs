using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class EmployeeTitle
    {
        public EmployeeTitle()
        {
            Employee = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}