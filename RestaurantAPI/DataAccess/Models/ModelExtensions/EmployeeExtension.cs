using DataAccess.DataTransferObjects;

namespace DataAccess.Models
{
    public partial class Employee
    {
        public static bool Validate(EmployeeDTO toValidate)
        {
            if (toValidate == null) return false;
            if (string.IsNullOrEmpty(toValidate.Address)) return false;
            if (string.IsNullOrEmpty(toValidate.Phone)) return false;
            if (string.IsNullOrEmpty(toValidate.ZipCode)) return false;
            if (string.IsNullOrEmpty(toValidate.FirstName)) return false;
            if (string.IsNullOrEmpty(toValidate.LastName)) return false;
            if (string.IsNullOrEmpty(toValidate.City)) return false;
            if (string.IsNullOrEmpty(toValidate.Email)) return false;
            if (string.IsNullOrEmpty(toValidate.Title)) return false;
            //TODO VALIDATION
            return true;
        }
    }
}
