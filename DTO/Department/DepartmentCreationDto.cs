using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspApp.DTO.Department
{
    public class DepartmentCreationDto
    {
        [Required(ErrorMessage ="Department is required")]
        [StringLength(50)]
        public string? Name {get; set;}

        [Required(ErrorMessage ="Status is required")]
        [StringLength(10)]
        public string? Status {get; set;}
        // public int ManagerId {get; set;}

    }
}