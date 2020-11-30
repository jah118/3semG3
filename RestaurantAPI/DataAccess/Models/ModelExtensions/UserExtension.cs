using DataAccess.DataTransferObjects;

namespace DataAccess.Models
{
    public partial class User
    {
        public static bool ValidateAddedUser(UserDTO toValidate)
        {
            //TODO make sure all cases are covered
            if (string.IsNullOrEmpty(toValidate.Username)) return false;
            if (toValidate.Username == null) return false;
            switch (toValidate.AccountType)
            {
                case UserRoles.Customer:
                    if (toValidate.Customer == null) return false;
                    break;
                case UserRoles.Employee:
                    if (toValidate.Employee == null) return false;
                    break;
                default: return false;
            }

            
            return true;
        }
    }
}