﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Business.Models
{
    public class LoginModel
    {
        public string EmployeeId { get; set; }
        public string Password { get; set; }
    }
}
