using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.BLL.Models
{

    public class Auth
    {
        public class Register
        {
            [Required]
            public string Firtsname { get; set; } = string.Empty;

            [Required]
            public string Lastname { get; set; } = string.Empty;

            [Required]
            public string Pseudo { get; set; } = string.Empty;

            [Required]
            public string Email { get; set; } = string.Empty;

            [Required]
            public string Password { get; set; } = string.Empty;
        }

        public class Login
        {
            [Required]
            public string Pseudo { get; set; } = string.Empty;

            [Required]
            public string Password { get; set; } = string.Empty;
        }
    }
}