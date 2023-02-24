using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspApp.DTO.Department
{
    public class DepartmentCreationDto
    {
        public string? Name {get; set;}
        public string? Status {get; set;}
    }
}