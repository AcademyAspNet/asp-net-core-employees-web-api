using System.ComponentModel.DataAnnotations;

namespace EmployeesWebAPI.Data.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        [MaxLength(64)]
        public required string FirstName { get; set; }

        [MaxLength(128)]
        public required string LastName { get; set; }

        [MaxLength(320)]
        public required string Email { get; set; }

        [MaxLength(128)]
        public required string Position { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
