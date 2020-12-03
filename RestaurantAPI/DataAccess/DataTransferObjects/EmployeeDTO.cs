namespace DataAccess.DataTransferObjects
{
    public class EmployeeDTO
    {
        public int Id { get; init; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
    }
}