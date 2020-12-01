using System.ComponentModel.DataAnnotations;

namespace RestaurantWebApp.DataTransferObject
{
    public class UserDTO
    {
        //
        //[Key, Column(Order = 1)]
        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        //public int idUser { get; }

        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Username { get; set; }
        public CustomerDTO Customer { get; set; }
        public EmployeeDTO Employee { get; set; }
        public UserRoles AccountType { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}