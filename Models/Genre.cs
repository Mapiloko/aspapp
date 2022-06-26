using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspApp.Controllers.Models
{
    public class Genre
    {
        public int Id {get; set;}
        [Required]
        public string? Name {get; set;}
        [Range(18, 120)]
        public int Age{get; set;}
        [CreditCard]
        public string? CreditCart{get; set;}
        [Url]
        public string? Url{get; set;}

    }
}