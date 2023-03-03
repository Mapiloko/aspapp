using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspapp.DTO;
using aspapp.DTO.Employee;
using aspapp.DTO.Utils;
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
        [Authorize(Policy = "AdminManagerEmployeePolicy")]
        public async Task<ActionResult<List<EmployeeDto>>> GetEmployees()
        {
            ResponseStatus response;
            // response = SetResponse(200, "No content","","");
            //         return StatusCode(200, response);
            try
            {
                var employees =  await _repo.GetEmployees();
                if(employees.Count == 0)
                {
                    response = SetResponse(200, "No content","","");
                    return StatusCode(200, response);
                }
                return Ok(employees);
            }
            catch(Exception ex)
            {
                response = SetResponse(400, ex.Message,"","");
                return BadRequest(response);
            }
        }

        [HttpGet("getbyid/{id:int}")]
        [Authorize(Policy = "AdminManagerEmployeePolicy")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            ResponseStatus response;
            try
            {
                var employee = await _repo.GetEmployeeById(id); 

                if(employee == null)
                {
                    return NoContent();
                }
                return Ok(employee);
            }
            catch(Exception ex)
            {
                response = SetResponse(400, ex.Message,"","");
                return BadRequest(response);
            }

        }

        [HttpPost("create")]
        [Authorize(Policy = "AdminPolicy")]
         public async Task<IActionResult> AddEmployee([FromBody] EmployeeCreationDto employeeCreationDto)
         {
            ResponseStatus response;
            try
            {
                var employeeCreated =  await _repo.AddEmployee(employeeCreationDto);

                if(!employeeCreated)
                {
                    response = SetResponse(205, "User with same username found, use different username","","");
                    return StatusCode(200, response);
                }

                response = SetResponse(200, "Employee Created","","");
                return Ok(response);
            }
            catch(Exception ex)
            {
                response = SetResponse(400, ex.Message,"","");
                return BadRequest(response);
            }

         }

         [HttpPut("update/{id:int}")]
         [Authorize(Policy = "AdminPolicy")]

         public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeCreationDto employeeCreationDto)
         {

            ResponseStatus response;
            try
            {
                var employee  = await _repo.UpdateEmployee(employeeCreationDto, id);

                if(employee == null)
                {
                    response = SetResponse(500, "Emplyee Update Failed!!","","");
                    return StatusCode(500, response);
                }

                response = SetResponse(200, "Employee Updated Successfully","","");
                return Ok(response);
            }
            catch(Exception ex)
            {
                response = SetResponse(400, ex.Message,"","");
                return BadRequest(response);
            }
         }

        [HttpPut("update/status/{id:int}")]
        [Authorize(Policy = "AdminManagerEmployeePolicy")]

         public async Task<IActionResult> ChangeStatus(int id, [FromBody] StatusEditDTO statusEditDTO)
         {
            ResponseStatus response;
            try
            {
                var employee  = await _repo.ChangeStatus(statusEditDTO, id);

                if(employee == null)
                {
                    response = SetResponse(500, "Status Change Failed!!","","");
                    return StatusCode(500, response);
                }

                response = SetResponse(200, "Status Changed Successfully","","");
                return Ok(response);
            }
            catch(Exception ex)
            {
                response = SetResponse(400, ex.Message,"","");
                return BadRequest(response);
            }

         }

         
        [HttpPut("edit/department/{id:int}")]
        [Authorize(Policy = "AdminPolicy")]
         public async Task<ActionResult> ChangeEmployeeDepartment(int id, [FromBody] EditDepartmentDTO editDepartmentDTO)
         {

            ResponseStatus response;
            try
            {
                var employee  = await _repo.ChangeEmployeeDepartment(editDepartmentDTO, id);

                if(employee == null)
                {
                    response = SetResponse(500, "Department Change Failed!!","","");
                    return StatusCode(500, response);
                }

                response = SetResponse(200, "Department Changed","","");
                return Ok(response);
            }
            catch(Exception ex)
            {
                response = SetResponse(400, ex.Message,"","");
                return BadRequest(response);
            }

         }

          /// Method to Set the Response
        private ResponseStatus SetResponse(int code, string message, string token, string role)
        {
            ResponseStatus response = new ResponseStatus()
            { 
               StatusCode = code,
               Message = message,
               Token = token,
               Role = role
            };
            return response;
        }
        
    }
}