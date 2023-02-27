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
        [Required]
        [StringLength(50)]
        public string? FirstName {get; set;}
        [Required]
        [StringLength(50)]
        public string? LastName {get; set;}

        [Phone]
        [Required]
        [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$")]
        public string? Telephone {get; set;}
        [Required]
        [EmailAddress]
        public string? Email {get; set;}
        [Required]
        public Boolean IsManager {get; set;} 
        [Required]
        public string? Status {get; set;} 
        [Required]
        public string? Password {get; set;} 
        public int DepartmentId {get; set;}
        [ForeignKeyAttribute("DepartmentId")]
        public Department? Department {get; set;}
    }
}