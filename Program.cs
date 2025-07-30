using EmployeesWebAPI.Data;
using EmployeesWebAPI.Data.Entities;
using EmployeesWebAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace EmployeesWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>();

            var app = builder.Build();

            app.MapGet("/api/v1/employees", ([FromServices] ApplicationDbContext database) =>
            {
                IList<Employee> employees = database.Employees.ToList();
                return Results.Ok(employees);
            });

            app.MapGet("/api/v1/employees/{id:int}", ([FromServices] ApplicationDbContext database, [FromRoute] int id) =>
            {
                Employee? employee = database.Employees.Find(id);

                if (employee == null)
                    return Results.NotFound();

                return Results.Ok(employee);
            });

            app.MapPost("/api/v1/employees", ([FromServices] ApplicationDbContext database, EmployeeDto employeeDto) =>
            {
                if (string.IsNullOrWhiteSpace(employeeDto.FirstName))
                    return Results.BadRequest("Incorrect first name");

                if (string.IsNullOrWhiteSpace(employeeDto.LastName))
                    return Results.BadRequest("Incorrect last name");

                if (string.IsNullOrWhiteSpace(employeeDto.Email))
                    return Results.BadRequest("Incorrect email address");

                if (string.IsNullOrWhiteSpace(employeeDto.Position))
                    return Results.BadRequest("Incorrect position");

                Employee employee = new Employee()
                {
                    FirstName = employeeDto.FirstName,
                    LastName = employeeDto.LastName,
                    Email = employeeDto.Email,
                    Position = employeeDto.Position
                };

                database.Employees.Add(employee);

                try
                {
                    database.SaveChanges();
                }
                catch (Exception exception) when (exception is DbUpdateException or DbUpdateConcurrencyException)
                {
                    return Results.Problem(
                        title: "Failed to create new employee",
                        statusCode: StatusCodes.Status500InternalServerError
                    );
                }

                return Results.Ok(employee);
            });

            app.MapDelete("/api/v1/employees/{id:int}", ([FromServices] ApplicationDbContext database, [FromRoute] int id) =>
            {
                Employee? employee = database.Employees.Find(id);

                if (employee == null)
                    return Results.NotFound();

                database.Employees.Remove(employee);

                try
                {
                    database.SaveChanges();
                }
                catch (Exception exception) when (exception is DbUpdateException or DbUpdateConcurrencyException)
                {
                    return Results.Problem(
                        title: $"Failed to delete employee with id {id}",
                        statusCode: StatusCodes.Status500InternalServerError
                    );
                }

                return Results.Ok();
            });

            app.MapPut("/api/v1/employees/{id:int}", (
                [FromServices] ApplicationDbContext database,
                [FromRoute] int id,
                EmployeeDto employeeDto
            ) =>
            {
                if (string.IsNullOrWhiteSpace(employeeDto.FirstName))
                    return Results.BadRequest("Incorrect first name");

                if (string.IsNullOrWhiteSpace(employeeDto.LastName))
                    return Results.BadRequest("Incorrect last name");

                if (string.IsNullOrWhiteSpace(employeeDto.Email))
                    return Results.BadRequest("Incorrect email address");

                if (string.IsNullOrWhiteSpace(employeeDto.Position))
                    return Results.BadRequest("Incorrect position");

                Employee? employee = database.Employees.Find(id);

                if (employee == null)
                    return Results.NotFound();

                employee.FirstName = employeeDto.FirstName;
                employee.LastName = employeeDto.LastName;
                employee.Email = employeeDto.Email;
                employee.Position = employeeDto.Position;

                database.Employees.Update(employee);

                try
                {
                    database.SaveChanges();
                }
                catch (Exception exception) when (exception is DbUpdateException or DbUpdateConcurrencyException)
                {
                    return Results.Problem(
                        title: $"Failed to update employee with id {id}",
                        statusCode: StatusCodes.Status500InternalServerError
                    );
                }

                return Results.Ok();
            });

            app.MapPatch("/api/v1/employees/{id:int}", (
                [FromServices] ApplicationDbContext database,
                [FromRoute] int id,
                [FromBody] EmployeeDto employeeDto
            ) =>
            {
                Employee? employee = database.Employees.Find(id);

                if (employee == null)
                    return Results.NotFound();

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
                    return Results.Ok();

                database.Employees.Update(employee);

                try
                {
                    database.SaveChanges();
                }
                catch (Exception exception) when (exception is DbUpdateException or DbUpdateConcurrencyException)
                {
                    return Results.Problem(
                        title: $"Failed to update employee with id {id}",
                        statusCode: StatusCodes.Status500InternalServerError
                    );
                }

                return Results.Ok();
            });

            app.Run();
        }
    }
}
