using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspApp.Models
{
    public class Admin
    {
        public int Id {get; set;}
        public string? Username {get; set;}
        public string? Password {get; set;}

    }
}