using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspapp.DTO.Accounts
{
    public class AuthResponseStatus
    {
        public class AuthStatus
        {
            public LoginStatus LoginStatus { get; set; }
            public string? Token { get; set; }
            public string? Role { get; set; }
        }
        public enum LoginStatus 
        {
            NoRoleToUser = 0,
            LoginFailed = 1,
            LoginSuccessful = 2
        }
        public class ResponseStatus
        {
            public int StatusCode { get; set; }
            public string? Message { get; set; }
            public string? Token { get; set; }
            public string? UserName { get; set; }
            public string? Role { get; set; }
        }
    }
}