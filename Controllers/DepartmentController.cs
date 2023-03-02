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
        [Authorize(Policy = "AdminManagerPolicy")]
        public async Task<ActionResult<List<DepartmentDto>>> GetDepartments()
        {
            var department =  await _repo.GetDepartments();
            if(department.Count == 0)
            {
                return NoContent();
            }
            
            return department;
        }

        [HttpGet("getbymanager/{id:int}")]
        [Authorize(Policy = "AdminManagerPolicy")]
        public async Task<ActionResult<EmployeeDto>> GetDepartmentByManager(int id)
        {
            var department = await _repo.GetDepartmentManager(id);
            if(department == null)
            {
                return NotFound();
            }
            return department;
        }

        [HttpGet("getbyid/{id:int}")]
        [Authorize(Policy = "AdminManagerPolicy")]
        public async Task<ActionResult<DepartmentDto>> GetDepartmentById(int id)
        {
            var department = await _repo.GetDepartmentById(id);
            if(department == null)
            {
                return NotFound();
            }
            return department;  //  _mapper.Map<DepartmentDto>(department);
        }

        [HttpPost("create")]
        [Authorize(Policy = "AdminPolicy")]
         public async Task<ActionResult<DepartmentDto>> Post([FromBody] DepartmentCreationDto departmentCreationDto)
         {
            var department = _mapper.Map<Department>(departmentCreationDto);
            var returnDepartment =  await _repo.AddDepartment(department);
            
            return _mapper.Map<DepartmentDto>(returnDepartment);
         }

         [HttpPut("update/{id:int}")]
         public async Task<ActionResult> Put(int id, [FromBody] DepartmentCreationDto departmentCreationDto)
         {
            var department  = await _repo.EditDepartment(departmentCreationDto, id);
            if(department == null)
            {
                return NotFound();
            }
            return NoContent();
         }

         [HttpPut("update/status/{id:int}")]
         public async Task<ActionResult> ChangeStatus(int id, [FromBody] StatusEditDTO statusEditDTO)
         {
            var department  = await _repo.ChangeStatus(statusEditDTO, id);
            if(department == null)
            {
                return NotFound();
            }
            return NoContent();
         }

    }
}