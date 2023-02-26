using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
         public ActionResult Put(int id, [FromBody] EmployeeCreationDto employeeCreationDto)
         {
            var employee = _mapper.Map<Employee>(employeeCreationDto);
            employee.Id = id;

            _repo.EditEmployee(employee);
            return NoContent();

         }

         [HttpDelete("{id:int}")]
         public ActionResult Delete(int id)
         {
            // var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);

            // if(employee == null)
            // {
            //     return NotFound();
            // }
            // _context.Remove(employee);
            // await _context.SaveChangesAsync();
            return NoContent();
         }

        
    }
}