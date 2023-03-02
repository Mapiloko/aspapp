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
        Task<List<EmployeeDto>> GetEmployees();
        Task<EmployeeDto> GetEmployeeById(int id);
        Task<Boolean> AddEmployee(EmployeeCreationDto employee);
        Task<Employee> UpdateEmployee(EmployeeCreationDto employee, int id);
        Task<Employee> ChangeStatus(StatusEditDTO status, int id);
        Task<Employee> ChangeEmployeeDepartment(EditDepartmentDTO department, int id);
    }
}