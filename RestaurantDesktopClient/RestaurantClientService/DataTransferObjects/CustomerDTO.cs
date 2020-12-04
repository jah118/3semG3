using System.Collections.Generic;

namespace RestaurantClientService.DataTransferObjects
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public virtual ICollection<ReservationDTO> Reservation { get; set; }
    }
}
