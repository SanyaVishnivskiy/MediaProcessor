﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.Auth
{
    public class CreateUserDTO
    {
        [Required]
        public string EmployeeId { get; set; }

        [Required]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords are different")]
        [DataType(DataType.Password)]
        public string ConfirmPassword{ get; set; }

        public List<string> Roles { get; set; } = new List<string>();
    }
}
