using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspapp.DTO;
using aspapp.DTO.Employee;
using AspApp.DTO.Employee;
using AspApp.Models;

namespace AspApp.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployeeById(int id);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> EditEmployee(EmployeeCreationDto employee, int id);
        Task<Employee> ChangeStatus(StatusEditDTO status, int id);
        Task<Employee> ChangeDepartmentManager(EditDepartmentDTO department, int id);
        Task<Employee> ChangeRole(EditRoleDTO role, int id);
    }
}