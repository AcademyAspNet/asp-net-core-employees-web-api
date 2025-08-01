using EmployeesWebAPI.Data.Entities;
using EmployeesWebAPI.Models.DTO;
using EmployeesWebAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EmployeesWebAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/departments")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger)
        {
            _departmentService = departmentService;
            _logger = logger;
        }

        [HttpGet]
        public IList<Department> GetDepartments()
        {
            return _departmentService.GetDepartments();
        }

        [HttpGet("{id:int}")]
        public Department? GetDepartmentById([FromRoute] int id)
        {
            return _departmentService.GetDepartmentById(id);
        }

        [HttpGet("{id:int}/employees")]
        public IActionResult GetDepartmentEmployees([FromRoute] int id)
        {
            Department? department = _departmentService.GetDepartmentById(id);

            if (department == null)
                return NotFound();

            return Ok(department.Employees);
        }

        [HttpPost]
        public IActionResult CreateDepartment([FromBody] DepartmentDto departmentDto)
        {
            try
            {
                Department department = _departmentService.CreateDepartment(departmentDto);
                return Ok(department);
            }
            catch (Exception exception) when (exception is DbUpdateException or DbUpdateConcurrencyException)
            {
                string errorMessage = exception.InnerException?.Message ?? exception.Message;
                _logger.LogError("Failed to create department: {0}", errorMessage);

                return Problem(
                    title: "Failed to create department",
                    detail: "An error occurred whil processing request",
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateDepartment([FromRoute] int id, [FromBody] DepartmentDto departmentDto)
        {
            Department? department = _departmentService.GetDepartmentById(id);

            if (department == null)
                return NotFound();

            department.Name = departmentDto.Name!;
            _departmentService.UpdateDepartment(department);

            return Ok(department);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteDepartment([FromRoute] int id)
        {
            bool isDeleted = _departmentService.DeleteDepartment(id);
            return isDeleted ? Ok() : NotFound();
        }
    }
}
