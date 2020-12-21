namespace RestaurantDesktopClient.DataTransferObject
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public CustomerDTO Customer { get; set; }

        public EmployeeDTO Employee { get; set; }

        public string AccountType {get; set;}
    }
}
