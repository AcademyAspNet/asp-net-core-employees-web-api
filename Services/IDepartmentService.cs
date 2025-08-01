using EmployeesWebAPI.Data.Entities;
using EmployeesWebAPI.Models.DTO;

namespace EmployeesWebAPI.Services
{
    public interface IDepartmentService
    {
        IList<Department> GetDepartments();
        Department? GetDepartmentById(int departmentId);
        Department CreateDepartment(DepartmentDto departmentDto);
        Department UpdateDepartment(Department department);
        bool DeleteDepartment(int departmentId);
        void DeleteDepartment(Department department);
    }
}
