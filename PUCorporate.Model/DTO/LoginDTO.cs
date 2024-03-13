using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUCorporate.Model.DTO
{
    public class LoginDTO
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string Password { get; set; }

         public string? Token { get; set; }

       // public string? Jwt { get; set; }
    }
}
