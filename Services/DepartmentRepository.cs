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
    public class DepartmentRepository: IDepartmentRepository
    {
        private readonly DatabaseContext _context;


        public DepartmentRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<List<Department>> GetDepartments()
        {
            var department =  await _context.Departments.ToListAsync();
            return department;
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            var department =  await _context.Departments.FirstOrDefaultAsync(x => x.Id == id);
            return department;
        }

        public async Task<Department> AddDepartment(Department department)
        {
            department.Status = "Active";
            _context.Add(department);
            await _context.SaveChangesAsync();

            return department;
        }
        public async void EditDepartment(Department department)
        {
            _context.Entry(department).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}