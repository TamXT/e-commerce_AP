﻿using System.ComponentModel.DataAnnotations;

namespace e_commerce_AP.Models
{
    public class LoginViewModel
    {
        [Required]
        public string UsernameOrEmail { get; set; } // Cho phép nhập vào username hoặc email

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
