using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace zesKod_mvc_core_auth.Models
{
    public class AuthModel
    {
    }
    public class LoginModel
    {
        [Required, MaxLength(20), MinLength(1)]
        public string username { get; set; }
        [Required, MaxLength(20), MinLength(1)]
        public string password { get; set; }
        public bool remember { get; set; }
    }
}
