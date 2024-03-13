using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUCorporate.Model.Model
{
    public class User
    {
        public int? LoginID { get; set; }
        [Required]
        public string? LoginName { get; set; }


        public string? Token { get; set; }
        public string? Jwt { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool? IsAdmin { get; set; }
        public string? PhoneExt { get; set; }
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }



    }
}
