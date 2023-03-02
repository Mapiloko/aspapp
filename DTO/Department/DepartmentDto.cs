using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspApp.DTO.Employee;

namespace AspApp.DTO.Department
{
    public class DepartmentDto
    {
        public int Id {get; set;}
        public string? Name {get; set;}
        public string? Status {get; set;}
        public string? Manager {get; set;}
        
    }
}