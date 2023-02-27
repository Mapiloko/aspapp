using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspApp.Models;
using AspApp.Interfaces;
using AspApp.Data;
using Microsoft.EntityFrameworkCore;
using AspApp.DTO.Employee;
using aspapp.DTO;
using aspapp.DTO.Employee;

namespace AspApp.Services
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private List<Employee> _employee;
        private readonly DatabaseContext _context;


        public EmployeeRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<List<Employee>> GetEmployees()
        {
            var employees =  await _context.Employees.ToListAsync();
            return employees;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            var employees =  await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            return employees;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            employee.Password = "Password123#";
            employee.Status = "Active";
            _context.Add(employee);
            await _context.SaveChangesAsync();

            return employee;
        }
        public async Task<Employee> EditEmployee(EmployeeCreationDto employeeCreationDto, int id)
        {
            var employee =  await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if(employee != null)
            {
                employee.FirstName = employeeCreationDto.FirstName;
                employee.LastName = employeeCreationDto.LastName;
                employee.Email = employeeCreationDto.Email;
                employee.Telephone = employeeCreationDto.Telephone;
                employee.DepartmentId = employeeCreationDto.DepartmentId;
                employee.Status = employeeCreationDto.Status;

                await _context.SaveChangesAsync();
            }
            return employee;
        }

        public async Task<Employee> ChangeStatus(StatusEditDTO statusEditDTO, int id)
        {
            var employee =  await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if(employee != null)
            {
                employee.Status = statusEditDTO.Status;
                await _context.SaveChangesAsync();
            }
            return employee;
        }

        public async Task<Employee> ChangeRole(EditRoleDTO editRoleDTO, int id)
        {
            var employee =  await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if(employee != null)
            {
                employee.IsManager = editRoleDTO.IsManager;
                await _context.SaveChangesAsync();
            }
            return employee;
        }
        public async Task<Employee> ChangeDepartmentManager(EditDepartmentDTO editDepartmentDto, int id)
        {
            var employee =  await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if(employee != null)
            {
                employee.DepartmentId = editDepartmentDto.DepartmentId;
                await _context.SaveChangesAsync();
            }
            return employee;
        }
    }
}