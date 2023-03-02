using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspapp.DTO;
using aspapp.DTO.Employee;
using aspapp.Services;
using AspApp.Data;
using AspApp.DTO.Employee;
using AspApp.Interfaces;
using AspApp.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspApp.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController: ControllerBase
    {
        private readonly IEmployeeRepository _repo;
        private readonly AuthSecurityService _authservice;


        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeRepository repo, IMapper mapper, AuthSecurityService authservice)
        {
            _mapper = mapper;
            _repo = repo;
            _authservice = authservice; 

        }

        [HttpGet("getall")]
        [Authorize(Policy = "AdminManagerPolicy")]
        public async Task<ActionResult<List<EmployeeDto>>> GetEmployees()
        {
            var employees =  await _repo.GetEmployees();
            if(employees.Count == 0)
            {
                return NoContent();
            }
            
            return employees;
        }
        [HttpGet("getbyid")]
        [Authorize(Policy = "AdminManagerEmployeePolicy")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int id)
        {
            var employee = await _repo.GetEmployeeById(id); 
            if(employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        [HttpPost("create")]
        [Authorize(Policy = "AdminPolicy")]
         public async Task<ActionResult<Boolean>> AddEmployee([FromBody] EmployeeCreationDto employeeCreationDto)
         {
            var employeeCreated =  await _repo.AddEmployee(employeeCreationDto);
            
            return employeeCreated;
         }

         [HttpPut("update/{id:int}")]
         [Authorize(Policy = "AdminPolicy")]

         public async Task<ActionResult> Put(int id, [FromBody] EmployeeCreationDto employeeCreationDto)
         {
            var employee  = await _repo.UpdateEmployee(employeeCreationDto, id);
            if(employee == null)
            {
                return NotFound();
            }

            return NoContent();
         }

        [HttpPut("update/status/{id:int}")]
        [Authorize(Policy = "AdminManagerEmployeePolicy")]

         public async Task<ActionResult> ChangeStatus(int id, [FromBody] StatusEditDTO statusEditDTO)
         {
            var employee  = await _repo.ChangeStatus(statusEditDTO, id);
            if(employee == null)
            {
                return NotFound();
            }
            return NoContent();

         }

         
        [HttpPut("edit/department/{id:int}")]
        [Authorize(Policy = "AdminPolicy")]
         public async Task<ActionResult> ChangeDepartmentManager(int id, [FromBody] EditDepartmentDTO editDepartmentDTO)
         {
            var employee  = await _repo.ChangeDepartmentManager(editDepartmentDTO, id);
            if(employee == null)
            {
                return NotFound();
            }
            return NoContent();

         }
        
    }
}