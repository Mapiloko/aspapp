using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspapp.DTO;
using AspApp.DTO.Department;
using AspApp.DTO.Employee;
using AspApp.Models;

namespace AspApp.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<List<DepartmentDto>> GetDepartments();
        Task<DepartmentDto> GetDepartmentById(int id);
        Task<EmployeeDto> GetDepartmentManager(int id);
        Task<Department> AddDepartment(Department department);
        Task<Department> EditDepartment(DepartmentCreationDto department, int id);
        Task<Department> ChangeStatus(StatusEditDTO status, int id);

    }
}