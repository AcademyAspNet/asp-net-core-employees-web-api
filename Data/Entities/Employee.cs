namespace EmployeesWebAPI.Data.Entities
{
    public class Employee : Entity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Position { get; set; }
    }
}
