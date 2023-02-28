using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspApp.Models
{
    public class Department
    {
        public int Id {get; set;}
        [RequiredAttribute]
        [StringLength(50)]
        public string? Name {get; set;}
        [RequiredAttribute]
        public string? Status {get; set;}
        [RequiredAttribute]
        public int ManagerId {get; set;}
        public List<Employee>? Employees {get; set;}

    }
}