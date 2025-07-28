using EmployeesWebAPI.Data;
using EmployeesWebAPI.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeesWebAPI
{
    class EmployeeDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Position { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                string? connectionString = builder.Configuration.GetConnectionString("Default");

                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new MissingFieldException("Failed to get Default connection string");

                options.UseSqlServer(connectionString);
            });

            var app = builder.Build();

            app.MapGet("/api/v1/employees", ([FromServices] ApplicationDbContext database) =>
            {
                IList<Employee> employees = database.Employees.ToList();
                return Results.Ok(employees);
            });

            app.MapPost("/api/v1/employees", ([FromServices] ApplicationDbContext database, EmployeeDto employeeDto) =>
            {
                Employee employee = new Employee()
                {
                    FirstName = employeeDto.FirstName,
                    LastName = employeeDto.LastName,
                    Email = employeeDto.Email,
                    Position = employeeDto.Position
                };

                database.Employees.Add(employee);
                database.SaveChanges();
            });

            app.Run();
        }
    }
}
