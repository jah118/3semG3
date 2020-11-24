using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataTransferObjects
{
    public enum UserRoles
    {
        Customer,
        Employee
    }

    public static class RoleDescriptor
    {
        public static string Describe(UserRoles role)
        {
            switch(role)
            {
                case UserRoles.Customer:
                    {
                        return "Customer";
                    }
                case UserRoles.Employee:
                    {
                        return "Employee";
                    }
                default: return null;
            }
        }
    }
}
