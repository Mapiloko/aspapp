using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspApp.DTO.Employee
{
    public class EmployeeDto
    {
        public int Id {get; set;}
        public string? FirstName {get; set;}
        public string? LastName {get; set;}

        public string? Telephone {get; set;}
        public string? Email {get; set;}
        public Boolean IsManager {get; set;}  
        public string? Status {get; set;}
        public int DepartmentId {get; set;}

    }
}