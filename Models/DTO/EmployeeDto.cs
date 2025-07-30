using System.ComponentModel.DataAnnotations;

namespace EmployeesWebAPI.Models.DTO
{
    public class EmployeeDto
    {
        [Required(ErrorMessage = "First name is not specified")]
        [MinLength(2, ErrorMessage = "Minimum first name length is {1} characters")]
        [MaxLength(64, ErrorMessage = "Maximum first name length is {1} characters")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is not specified")]
        [MinLength(2, ErrorMessage = "Minimum last name length is {1} characters")]
        [MaxLength(128, ErrorMessage = "Maximum last name length is {1} characters")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is not specified")]
        [MinLength(5, ErrorMessage = "Minimum email length is {1} characters")]
        [MaxLength(320, ErrorMessage = "Maximum email length is {1} characters")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Position is not specified")]
        [MinLength(2, ErrorMessage = "Minimum position length is {1} characters")]
        [MaxLength(128, ErrorMessage = "Maximum position length is {1} characters")]
        public string? Position { get; set; }
    }
}
