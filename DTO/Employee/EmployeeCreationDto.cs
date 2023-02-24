using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspApp.DTO.Employee
{
    public class EmployeeCreationDto
    {
        public string? FirstName {get; set;}
        public string? LastName {get; set;}
        public string? Email {get; set;}

        public string? Telephone {get; set;}
        public int Manager {get; set;} 
        public string? Status {get; set;}
        public string? Role {get; set;} 

    }
}