using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataTransferObjects
{
    public enum UserRoles
    {
        Undefined,
        Customer,
        Employee
    }

    public static class RoleDescriptor
    {
        public static string Describe(UserRoles role)
        {
            return role switch
            {
                UserRoles.Customer => "Customer",
                UserRoles.Employee => "Employee",
                _ => null,
            };
        }

        public static UserRoles Parse(string roleText)
        {
            return roleText switch
            {
                "Customer" => UserRoles.Customer,
                "Employee" => UserRoles.Employee,
                _ => UserRoles.Customer,
            };
        }
    }
}
