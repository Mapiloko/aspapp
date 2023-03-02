using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspApp.DTO.Employee
{
    public class EmployeeCreationDto
    {
        [Required(ErrorMessage = "First name is Required")]
        public string? FirstName {get; set;}
        [Required(ErrorMessage = "Last name is Required")]
        public string? LastName {get; set;}

        [Required(ErrorMessage = "Email address is Required")]        
        [EmailAddress]
        public string? Email {get; set;}

        [Phone]
        [Required]
        [StringLength(10)]
        public string? Telephone {get; set;}
        [Required(ErrorMessage = "Status is Required")]
        public string? Status {get; set;}
        public string? Role {get; set;}
        [Required(ErrorMessage = "DepartmentId is Required")]
        public int DepartmentId {get; set;}

    }
}