using DataAccess.DataTransferObjects;

namespace DataAccess.Models
{
    public partial class Customer
    {
        public static bool Validate(CustomerDTO toValidate)
        {
            if (toValidate == null) return false;
            if (string.IsNullOrEmpty(toValidate.Address)) return false;
            if (string.IsNullOrEmpty(toValidate.Phone)) return false;
            if (string.IsNullOrEmpty(toValidate.ZipCode)) return false;
            if (string.IsNullOrEmpty(toValidate.FirstName)) return false;
            if (string.IsNullOrEmpty(toValidate.LastName)) return false;
            if (string.IsNullOrEmpty(toValidate.City)) return false;
            if (string.IsNullOrEmpty(toValidate.Email)) return false;
            //TODO VALIDATION
            return true;
        }
    }
}
