using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspApp.Models;
using AspApp.Data;
using AspApp.DTO.Department;
using AspApp.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using aspapp.DTO;
using AspApp.DTO.Employee;
using aspapp.DTO.Utils;

namespace AspApp.Controllers
{
    [Route("api/department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _repo;
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        public DepartmentController(IDepartmentRepository repo, DatabaseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _repo = repo;

        }

        [HttpGet("getall")]
        [Authorize(Policy = "AdminManagerEmployeePolicy")]
        public async Task<IActionResult> GetDepartments()
        {
            ResponseStatus response;
            try
            {
                var department =  await _repo.GetDepartments();
                if (department.Count == 0)
                {
                    response = SetResponse(200, "No content","","");
                    return StatusCode(200, response);
                }
                return Ok(department);
            }
            catch(Exception ex)
            {
                response = SetResponse(400, ex.Message,"","");
                return BadRequest(response);
            }
        }

        [HttpGet("getbymanager/{id:int}")]
        [Authorize(Policy = "AdminManagerEmployeePolicy")]
        public async Task<IActionResult> GetDepartmentByManager(int id)
        {
            ResponseStatus response;
            try
            {
                var department = await _repo.GetDepartmentManager(id);
                if(department == null)
                {
                    response = SetResponse(500, "Department Not Found","","");
                    return StatusCode(500, response);
                }
                return Ok(department);
            }
            catch(Exception ex)
            {
                response = SetResponse(400, ex.Message,"","");
                return BadRequest(response);
            }

            // var department = await _repo.GetDepartmentManager(id);
            // if(department == null)
            // {
            //     return NotFound();
            // }
            // return department;
        }

        [HttpGet("getbyid/{id:int}")]
        [Authorize(Policy = "AdminManagerEmployeePolicy")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            ResponseStatus response;
            try
            {
                var department = await _repo.GetDepartmentById(id);
                if(department == null)
                {
                    response = SetResponse(500, "Department Not Found","","");
                    return StatusCode(500, response);
                }
                return Ok(department);
            }
            catch(Exception ex)
            {
                response = SetResponse(400, ex.Message,"","");
                return BadRequest(response);
            }
            
            // var department = await _repo.GetDepartmentById(id);
            // if(department == null)
            // {
            //     return NotFound();
            // }
            // return department;  
        }

        [HttpPost("create")]
        [Authorize(Policy = "AdminManagerEmployeePolicy")]
         public async Task<IActionResult> Post([FromBody] DepartmentCreationDto departmentCreationDto)
         {
            ResponseStatus response;
            try
            {
                var department = _mapper.Map<Department>(departmentCreationDto);
                var returndpt =  await _repo.AddDepartment(department);
                if(returndpt == null)
                {
                    response = SetResponse(500, "Failed to Create new Department","","");
                    return StatusCode(500, response);
                }

                response = SetResponse(200, "Department Created","","");
                return Ok(returndpt);
            }
            catch(Exception ex)
            {
                response = SetResponse(400, ex.Message,"","");
                return BadRequest(response);
            }

         }

        [HttpPut("update/{id:int}")]
        [Authorize(Policy = "AdminManagerEmployeePolicy")]
         public async Task<IActionResult> Put(int id, [FromBody] DepartmentCreationDto departmentCreationDto)
         {
            ResponseStatus response;
            try
            {
                var department  = await _repo.EditDepartment(departmentCreationDto, id);
                if(department == null)
                {
                    response = SetResponse(500, "Department Not Found","","");
                    return StatusCode(500, response);
                }
                response = SetResponse(200, "Department Updated","","");
                return Ok(response);

            }
            catch(Exception ex)
            {
                response = SetResponse(400, ex.Message,"","");
                return BadRequest(response);
            }

            // var department  = await _repo.EditDepartment(departmentCreationDto, id);
            // if(department == null)
            // {
            //     return NotFound();
            // }
            // return NoContent();
         }

        [HttpPut("update/status/{id:int}")]
        [Authorize(Policy = "AdminManagerEmployeePolicy")]

         public async Task<IActionResult> ChangeStatus(int id, [FromBody] StatusEditDTO statusEditDTO)
         {
            ResponseStatus response;
            try
            {
                var department  = await _repo.ChangeStatus(statusEditDTO, id);
                if(department == null)
                {
                    response = SetResponse(500, "Department Not Found","","");
                    return StatusCode(500, response);
                }
                response = SetResponse(200, "Department Status Chnaged","","");
                return Ok(response);
            }
            catch(Exception ex)
            {
                response = SetResponse(400, ex.Message,"","");
                return BadRequest(response);
            }

            // var department  = await _repo.ChangeStatus(statusEditDTO, id);
            // if(department == null)
            // {
            //     return NotFound();
            // }
            // return NoContent();
         }

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