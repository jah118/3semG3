using DataAccess.DataTransferObjects;

namespace DataAccess.Models
{
    public partial class User
    {
        public static bool Validate(UserDTO toValidate)
        {
            if (toValidate == null) return false;
            if (string.IsNullOrEmpty(toValidate.Username)) return false;
            if (toValidate.Username == null) return false;
            switch (toValidate.AccountType)
            {
                case UserRoles.Customer:
                    if (!Customer.Validate(toValidate.Customer)) return false;
                    break;
                case UserRoles.Employee:
                    if (!Employee.Validate(toValidate.Employee)) return false;
                    break;
                default: return false;
            }
            return true;
        }
    }
}