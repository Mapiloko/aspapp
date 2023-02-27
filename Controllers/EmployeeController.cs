using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspapp.DTO;
using aspapp.DTO.Employee;
using AspApp.Data;
using AspApp.DTO.Employee;
using AspApp.Interfaces;
using AspApp.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AspApp.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController: ControllerBase
    {
        private readonly IEmployeeRepository _repo;

        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeDto>>> Get()
        {
            var employees =  await _repo.GetEmployees();
            if(employees.Count == 0)
            {
                return NoContent();
            }
            
            return _mapper.Map<List<EmployeeDto>>(employees);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmployeeDto>> Get(int id)
        {
            var employee = await _repo.GetEmployeeById(id); // .Employees.FirstOrDefaultAsync(x => x.Id == id );
            if(employee == null)
            {
                return NotFound();
            }
            return _mapper.Map<EmployeeDto>(employee);
        }

        [HttpPost]
         public async Task<ActionResult<EmployeeDto>> Post([FromBody] EmployeeCreationDto employeeCreationDto)
         {
            var employee = _mapper.Map<Employee>(employeeCreationDto);
            var returnEmployee =  await _repo.AddEmployee(employee);
            
            return _mapper.Map<EmployeeDto>(returnEmployee);
         }

         [HttpPut("{id:int}")]
         public async Task<ActionResult> Put(int id, [FromBody] EmployeeCreationDto employeeCreationDto)
         {
            var employee  = await _repo.EditEmployee(employeeCreationDto, id);
            if(employee == null)
            {
                return NotFound();
            }
            return NoContent();

         }

         [HttpPut("status/{id:int}")]
         public async Task<ActionResult> ChangeStatus(int id, [FromBody] StatusEditDTO statusEditDTO)
         {
            var employee  = await _repo.ChangeStatus(statusEditDTO, id);
            if(employee == null)
            {
                return NotFound();
            }
            return NoContent();

         }

         [HttpPut("editrole/{id:int}")]
         public async Task<ActionResult> ChangeRole(int id, [FromBody] EditRoleDTO editRoleDTO)
         {
            var employee  = await _repo.ChangeRole(editRoleDTO, id);
            if(employee == null)
            {
                return NotFound();
            }
            return NoContent();

         }
         
         [HttpPut("department/{id:int}")]
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