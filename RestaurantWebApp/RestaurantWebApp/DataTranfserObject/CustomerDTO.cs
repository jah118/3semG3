using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
    public class CustomerDTO
    {
        public CustomerDTO()
        {
        }

        public int Id { get; }

        [Display(Name = "Tlf")]
        //TODO lav bedre regex[RegularExpression("^([+](\\d{1,3})\\s?)?((\\(\\d{3,5}\\)|\\d{3,5})(\\s)?)\\d{3,8}$", ErrorMessage = "Tast")]

        public string Phone { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [EmailAddress, Display(Name = "Navn")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }

    }
}
