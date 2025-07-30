using EmployeesWebAPI.Data;
using EmployeesWebAPI.Data.Entities;
using EmployeesWebAPI.Filters;
using EmployeesWebAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeesWebAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _database;

        public EmployeeController(ApplicationDbContext database)
        {
            _database = database;
        }

        [HttpGet]
        public IList<Employee> GetEmployees()
        {
            return _database.Employees.ToList();
        }

        [HttpGet("{id:int}")]
        public IActionResult GetEmployeeById([FromRoute] int id)
        {
            Employee? employee = _database.Employees.Find(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            Employee employee = new Employee()
            {
                FirstName = employeeDto.FirstName!,
                LastName = employeeDto.LastName!,
                Email = employeeDto.Email!,
                Position = employeeDto.Position!
            };

            _database.Employees.Add(employee);

            try
            {
                _database.SaveChanges();
            }
            catch (Exception exception) when (exception is DbUpdateException or DbUpdateConcurrencyException)
            {
                return Problem(
                    title: "Failed to create new employee",
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }

            return Ok(employee);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteEmployee([FromRoute] int id)
        {
            Employee? employee = _database.Employees.Find(id);

            if (employee == null)
                return NotFound();

            _database.Employees.Remove(employee);

            try
            {
                _database.SaveChanges();
            }
            catch (Exception exception) when (exception is DbUpdateException or DbUpdateConcurrencyException)
            {
                return Problem(
                    title: $"Failed to delete employee with id {id}",
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }

            return Ok();
        }

        [HttpPut("{id:int}")]
        public IActionResult FullUpdateEmployee([FromRoute] int id, [FromBody] EmployeeDto employeeDto)
        {
            Employee? employee = _database.Employees.Find(id);

            if (employee == null)
                return NotFound();

            employee.FirstName = employeeDto.FirstName!;
            employee.LastName = employeeDto.LastName!;
            employee.Email = employeeDto.Email!;
            employee.Position = employeeDto.Position!;

            _database.Employees.Update(employee);

            try
            {
                _database.SaveChanges();
            }
            catch (Exception exception) when (exception is DbUpdateException or DbUpdateConcurrencyException)
            {
                return Problem(
                    title: $"Failed to update employee with id {id}",
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }

            return Ok();
        }

        [HttpPatch("{id:int}")]
        [DisableModelValidation]
        public IActionResult PartialUpdateEmployee([FromRoute] int id, [FromBody] EmployeeDto employeeDto)
        {
            Employee? employee = _database.Employees.Find(id);

            if (employee == null)
                return NotFound();

            bool isSomethingChanged = false;

            if (!string.IsNullOrWhiteSpace(employeeDto.FirstName))
            {
                employee.FirstName = employeeDto.FirstName;
                isSomethingChanged = true;
            }

            if (!string.IsNullOrWhiteSpace(employeeDto.LastName))
            {
                employee.LastName = employeeDto.LastName;
                isSomethingChanged = true;
            }

            if (!string.IsNullOrWhiteSpace(employeeDto.Email))
            {
                employee.Email = employeeDto.Email;
                isSomethingChanged = true;
            }

            if (!string.IsNullOrWhiteSpace(employeeDto.Position))
            {
                employee.Position = employeeDto.Position;
                isSomethingChanged = true;
            }

            if (!isSomethingChanged)
                return Ok();

            _database.Employees.Update(employee);

            try
            {
                _database.SaveChanges();
            }
            catch (Exception exception) when (exception is DbUpdateException or DbUpdateConcurrencyException)
            {
                return Problem(
                    title: $"Failed to update employee with id {id}",
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }

            return Ok();
        }
    }
}
