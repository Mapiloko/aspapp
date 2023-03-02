using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aspapp.DTO.Accounts
{
    public class UserModels
    {
        /// <summary>
    /// Class for Login Information
    /// </summary>
    public class LoginUser
    {
        [Required(ErrorMessage = "User Name is Required")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string? Password { get; set; }
         
    }

    /// <summary>
    /// Class for Registering User
    /// </summary>
    public class RegisterUser
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string? Email { get; set; }
        
        [Required(ErrorMessage = "Password is Required")]
        public string? Password { get; set; }
  
    }

    public class ApplicationRole
    {
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }
    }

    /// <summary>
    /// Class for Approving User to Assign Role to it
    /// </summary>
    public class UserRole
    {
        public string? UserName { get; set; }
        public string? RoleName { get; set; }
    }
    public class UserUpdate
    {
        public string? UserName { get; set; }
        public string? NewUserName { get; set; }
        public string? PhoneNumber { get; set; }
    }
    }
}