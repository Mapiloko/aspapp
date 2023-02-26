using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspApp.Models;
using AspApp.Interfaces;
using AspApp.Data;
using Microsoft.EntityFrameworkCore;
using AspApp.DTO.Department;
using AutoMapper;
using aspapp.DTO;

namespace AspApp.Services
{
    public class DepartmentRepository: IDepartmentRepository
    {
        private readonly DatabaseContext _context;
        public DepartmentRepository(DatabaseContext context )
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
        public async Task<Department> EditDepartment(DepartmentCreationDto departmentCreationDto, int id)
        {
            var department =  await _context.Departments.FirstOrDefaultAsync(x => x.Id == id);
            if(department != null)
            {
                department.Name = departmentCreationDto.Name;
                department.ManagerId = departmentCreationDto.ManagerId;
                department.Status = departmentCreationDto.Status;

                await _context.SaveChangesAsync();
            }

            return department;
        }
        public async Task<Department> ChangeStatus(StatusEditDTO statusEditDTO, int id)
        {
            var department =  await _context.Departments.FirstOrDefaultAsync(x => x.Id == id);
            if(department != null)
            {
                department.Status = statusEditDTO.Status;
                await _context.SaveChangesAsync();
            }

            return department;
        }
    }
}