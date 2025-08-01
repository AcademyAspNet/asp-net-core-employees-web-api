using EmployeesWebAPI.Data;
using EmployeesWebAPI.Data.Entities;
using EmployeesWebAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace EmployeesWebAPI.Services.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _database;

        public DepartmentService(ApplicationDbContext database)
        {
            _database = database;
        }

        public IList<Department> GetDepartments()
        {
            return _database.Departments.ToList();
        }

        public Department? GetDepartmentById(int departmentId)
        {
            return _database.Departments.Include(d => d.Employees)
                                        .Where(d => d.Id == departmentId)
                                        .FirstOrDefault();
        }

        public Department CreateDepartment(DepartmentDto departmentDto)
        {
            if (departmentDto == null)
                throw new NullReferenceException("Department DTO is null");

            if (departmentDto.Name == null)
                throw new ArgumentNullException(nameof(departmentDto));

            Department department = new Department()
            {
                Name = departmentDto.Name
            };

            _database.Departments.Add(department);
            _database.SaveChanges();

            return department;
        }

        public Department UpdateDepartment(Department department)
        {
            _database.Departments.Update(department);
            _database.SaveChanges();

            return department;
        }

        public bool DeleteDepartment(int departmentId)
        {
            int affectedRows = _database.Departments.Where(d => d.Id == departmentId)
                                                    .ExecuteDelete();

            return affectedRows > 0;
        }

        public void DeleteDepartment(Department department)
        {
            _database.Departments.Remove(department);
            _database.SaveChanges();
        }
    }
}
