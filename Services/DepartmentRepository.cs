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
using AspApp.DTO.Employee;
using Microsoft.AspNetCore.Identity;
using aspapp.Services;

namespace AspApp.Services
{
    public class DepartmentRepository: IDepartmentRepository
    {
        private readonly DatabaseContext _context;
        RoleManager<IdentityRole> _roleManager;
        UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly AuthSecurityService _authSecurityService;
        public DepartmentRepository(DatabaseContext context, RoleManager<IdentityRole> roleManager,
         UserManager<IdentityUser> userManager, IMapper mapper, AuthSecurityService authSecurityService )
        {
            _context = context;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
            _authSecurityService = authSecurityService;
        }
        public async Task<List<DepartmentDto>> GetDepartments()
        {
            var departments =  await _context.Departments.ToListAsync();
            var departmentDtos =  _mapper.Map<List<DepartmentDto>>(departments);
            foreach (var departmentDto in departmentDtos)
            {
                EmployeeDto manager =  await GetDepartmentManager(departmentDto.Id);
                if(manager != null)
                    departmentDto.Manager = manager.FirstName +" "+ manager.LastName;
                else
                    departmentDto.Manager = "Not Assigned";
            }
            return departmentDtos;
        }

        public async Task<DepartmentDto> GetDepartmentById(int id)
        {
            var department =  await _context.Departments.FirstOrDefaultAsync(x => x.Id == id);
            DepartmentDto departmentDto = null;
            
            if(department != null)
            {
                departmentDto =  _mapper.Map<DepartmentDto>(department);
                EmployeeDto manager =  await GetDepartmentManager(departmentDto.Id);
                if(manager != null)
                    departmentDto.Manager = manager.FirstName +" "+ manager.LastName;
                else
                    departmentDto.Manager = "Not Assigned";
            }

            return departmentDto;
        }

        public async Task<EmployeeDto> GetDepartmentManager(int id)
        {
            var departmentEmployees =  await _context.Employees.Where(x => x.DepartmentId == id).ToListAsync();
            EmployeeDto departmentManager  = null;

            foreach (Employee employee in departmentEmployees)
            {
                var user = await _userManager.FindByEmailAsync(employee.Email);
                var roles = await _userManager.GetRolesAsync(user);
                if(roles[0] == "Manager")
                {
                    departmentManager = _mapper.Map<EmployeeDto>(employee);
                    departmentManager.Telephone = user.PhoneNumber;
                    departmentManager.Role = roles[0];
                }
            } 
            return departmentManager;
        }

        public async Task<Department> AddDepartment(Department department)
        {
            // department.Status = "Active";
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
                // department.ManagerId = departmentCreationDto.ManagerId;
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