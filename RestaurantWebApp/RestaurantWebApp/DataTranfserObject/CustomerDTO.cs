using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataTransferObjects
{
    public class CustomerDTO
    {
        public CustomerDTO()
        {
        }

        public int Id { get;}

        [Display(Name = "Tlf")]
        //TODO lav bedre regex[RegularExpression("^([+](\\d{1,3})\\s?)?((\\(\\d{3,5}\\)|\\d{3,5})(\\s)?)\\d{3,8}$", ErrorMessage = "Tast")]
        public string Phone { get; set; }
        [EmailAddress, Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Navn")]
        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Address { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string ZipCode { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string City { get; set; }

    }
}
