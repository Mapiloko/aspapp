using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspapp.DTO.Accounts
{
    public class AuthenticationResponse
    {
        public string? Token {get;set;}
        public DateTime Expiration {get;set;}
    }
}