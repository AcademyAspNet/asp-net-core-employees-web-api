using EmployeesWebAPI.Data;
using EmployeesWebAPI.Data.Entities;
using EmployeesWebAPI.Models.DTO;
using EmployeesWebAPI.Services;
using EmployeesWebAPI.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeesWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>();
            builder.Services.AddControllers();

            builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            var app = builder.Build();

            app.UseRouting();
            app.MapControllers();

            app.Run();
        }
    }
}
