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
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using static aspapp.DTO.Accounts.UserModels;
using aspapp.Services;

namespace AspApp.Services
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private List<Employee> _employee;
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly AuthSecurityService _authSecurityService;
        RoleManager<IdentityRole> _roleManager;
        UserManager<IdentityUser> _userManager;


        public EmployeeRepository(DatabaseContext context, IMapper mapper,
         RoleManager<IdentityRole> roleManager,
         UserManager<IdentityUser> userManager, AuthSecurityService authSecurityService)
        {
            _context = context;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
            _authSecurityService = authSecurityService;
        }
        public async Task<List<EmployeeDto>> GetEmployees()
        {
            var employees =  await _context.Employees.ToListAsync();
            var employeeDtos = new List<EmployeeDto>();

            if(employees.Count == 0)
            {
                return employeeDtos;
            }

            employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);
            foreach(EmployeeDto employeeDto in employeeDtos )
            {
                var user = await _userManager.FindByEmailAsync(employeeDto.Email);
                var roles = await _userManager.GetRolesAsync(user);
                employeeDto.Telephone = user.PhoneNumber;
                employeeDto.Email = user.Email;
                employeeDto.Role = roles[0];
            }

            return employeeDtos;
        }

        public async Task<EmployeeDto> GetEmployeeById(int id)
        {
            var employee =  await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            EmployeeDto employeeDto = null;
            if(employee != null)
            {
                var user = await _userManager.FindByEmailAsync(employee.Email);
                var roles = await _userManager.GetRolesAsync(user);
                employeeDto = _mapper.Map<EmployeeDto>(employee);
                employeeDto.Telephone = user.PhoneNumber;
                employeeDto.Email = user.Email;
                employeeDto.Role = roles[0];
            }

            return employeeDto;
        }

        public async Task<Boolean> AddEmployee(EmployeeCreationDto employeeCreationDto)
        {

            var user = await _userManager.FindByEmailAsync(employeeCreationDto.Email);

            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = employeeCreationDto.Email,
                    Email = employeeCreationDto.Email,
                    PhoneNumber = employeeCreationDto.Telephone,
                    PhoneNumberConfirmed = true,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(user, "Password123#");

                if (result.Succeeded)
                {
                    result = await _userManager.AddToRoleAsync(user, employeeCreationDto.Role);
                }

                // Change previous Manager
                var departmentEmployees =  await _context.Employees.Where(x => x.DepartmentId == employeeCreationDto.DepartmentId).ToListAsync();
                // if(departmentEmployees.Count != 0)
                // {
                    foreach (Employee employee_ in departmentEmployees)
                    {
                        user = await _userManager.FindByEmailAsync(employee_.Email);
                        var roles = await _userManager.GetRolesAsync(user);
                        if(roles[0] == "Manager" && employeeCreationDto.Role == "Manager")
                        {
                        var userRole = new UserRole(){ RoleName = "Employee", UserName = employee_.Email };
                        var roleChanged = await _authSecurityService.ChangeRole(userRole);
                        }
                    }
                // }
                // else
                // {

                // }
                

                var employee = _mapper.Map<Employee>(employeeCreationDto);
                employee.Status = "Active";
                _context.Add(employee);
                await _context.SaveChangesAsync();

               return true;
            }
            return false;
        }
        public async Task<Employee> UpdateEmployee(EmployeeCreationDto employeeCreationDto, int id)
        {
            var employee =  await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if(employee != null)
            {
                var userUpdate = new UserUpdate(){ UserName = employee.Email, NewUserName =  employeeCreationDto.Email, PhoneNumber = employeeCreationDto.Telephone };
                var updated = await _authSecurityService.UpdateUser(userUpdate);

                employee.FirstName = employeeCreationDto.FirstName;
                employee.LastName = employeeCreationDto.LastName;
                employee.Email = employeeCreationDto.Email;
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

        public async Task<Employee> ChangeEmployeeDepartment(EditDepartmentDTO editDepartmentDto, int id)
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