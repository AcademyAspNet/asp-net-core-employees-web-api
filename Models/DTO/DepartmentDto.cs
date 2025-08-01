using System.ComponentModel.DataAnnotations;

namespace EmployeesWebAPI.Models.DTO
{
    public class DepartmentDto
    {
        [Required(ErrorMessage = "Department name is not specified")]
        [MinLength(2, ErrorMessage = "Minimum department name length is {1} characters")]
        [MaxLength(128, ErrorMessage = "Maximum department name length is {1} characters")]
        public string? Name { get; set; }
    }
}
