using System.Collections.Generic;

namespace DataAccess.DataTransferObjects
{
    public class CustomerDTO
    {
        public CustomerDTO()
        {
        }

        public CustomerDTO(int id)
        {
            Id = id;
        }

        public int Id { get; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }

        public virtual ICollection<ReservationDTO> Reservation { get; set; }
    }
}