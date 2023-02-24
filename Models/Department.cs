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
        [Required]
        [StringLength(2)]
        public string? Name {get; set;}
        [Required]
        public string? Status {get; set;}

    }
}