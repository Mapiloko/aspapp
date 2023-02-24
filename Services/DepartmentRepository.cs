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
        private List<Department> _department;
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
            _context.Add(department);
            await _context.SaveChangesAsync();

            return department;
        }
    }
}