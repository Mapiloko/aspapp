using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspapp.DTO.Utils
{
    public class ResponseStatus
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }

    public class AuthResponseStatus
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; }
        public string? UserName { get; set; }
        public string? Role { get; set; }
    }
}