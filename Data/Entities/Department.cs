using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeesWebAPI.Data.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Department
    {
        public int Id { get; set; }

        [MaxLength(128)]
        public required string Name { get; set; }

        [JsonIgnore]
        public IList<Employee> Employees { get; set; } = new List<Employee>();

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
