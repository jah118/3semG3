using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAccess.DataTransferObjects;

namespace RestaurantAPI.Models
{
    public record LoginInfo :IValidatableObject 
    {
        public UserDTO User { get; init; }
        public string Username { get; init; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; init; }
        public UserRoles Role { get; init; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(User.Username))
                yield return new ValidationResult("Body must contain a Username, either in the form of a UserDTO or a plain string");
        }
    }
}