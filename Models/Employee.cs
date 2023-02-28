using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspApp.Models
{
    public class Employee
    {
        [Key]
        public int Id {get; set;}
        [RequiredAttribute]
        [StringLength(50)]
        public string? FirstName {get; set;}
        [RequiredAttribute]
        [StringLength(50)]
        public string? LastName {get; set;}

        [Phone]
        [RequiredAttribute]
        [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$")]
        public string? Telephone {get; set;}
        [RequiredAttribute]
        [EmailAddress]
        public string? Email {get; set;}
        [RequiredAttribute]
        public Boolean IsManager {get; set;} 
        [RequiredAttribute]
        public string? Status {get; set;} 
        [RequiredAttribute]
        public string? Password {get; set;} 
        public int DepartmentId {get; set;}
        [ForeignKeyAttribute("DepartmentId")]
        public Department? Department {get; set;}
    }
}