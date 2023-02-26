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

        [HttpGet]
        public async Task<ActionResult<List<DepartmentDto>>> Get()
        {
            var department =  await _repo.GetDepartments();
            if(department.Count == 0)
            {
                return NoContent();
            }
            
            return _mapper.Map<List<DepartmentDto>>(department);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DepartmentDto>> Get(int id)
        {
            var department = await _repo.GetDepartmentById(id);
            if(department == null)
            {
                return NotFound();
            }
            return _mapper.Map<DepartmentDto>(department);
        }

        [HttpPost]
         public async Task<ActionResult<DepartmentDto>> Post([FromBody] DepartmentCreationDto departmentCreationDto)
         {
            var department = _mapper.Map<Department>(departmentCreationDto);
            var returnDepartment =  await _repo.AddDepartment(department);
            
            return _mapper.Map<DepartmentDto>(returnDepartment);
         }

         [HttpPut("{id:int}")]
         public ActionResult Put(int id, [FromBody] DepartmentCreationDto genreCreationDto)
         {
            var department = _mapper.Map<Department>(genreCreationDto);
            department.Id = id;

            _repo.EditDepartment(department);
            
            return NoContent();
         }

         [HttpDelete("{id:int}")]
         public ActionResult Delete(int id)
         {
            // var department = await _context.Departments.FirstOrDefaultAsync(x => x.Id == id);

            // if(department == null)
            // {
            //     return NotFound();
            // }
            // _context.Remove(department);
            // await _context.SaveChangesAsync();
            return NoContent();
         }

    }
}