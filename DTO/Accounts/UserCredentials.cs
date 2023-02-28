using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspapp.DTO.Accounts
{
    public class UserCredentials
    {
        [RequiredAttribute]
        [EmailAddress]
        public string? Email {get; set;}
        [RequiredAttribute]
        public string? Password {get; set;}
    }
}