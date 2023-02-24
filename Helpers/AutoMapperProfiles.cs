using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspApp.DTO.Employee;
using AspApp.DTO.Department;
using AspApp.Models;
using AutoMapper;

namespace AspApp.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<EmployeeDto,Employee>().ReverseMap();
            CreateMap<EmployeeCreationDto,Employee>();
            CreateMap<DepartmentDto,Department>().ReverseMap();
            CreateMap<DepartmentCreationDto,Department>();
        }        
    }
}