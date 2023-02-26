using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspApp.Models;
using AspApp.Interfaces;
using AspApp.Data;
using Microsoft.EntityFrameworkCore;

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
        public async void EditEmployee(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}