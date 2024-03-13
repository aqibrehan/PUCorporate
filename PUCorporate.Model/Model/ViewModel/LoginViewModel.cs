using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUCorporate.Model.Model.ViewModel
{
    public class LoginViewModel
    {
      
        public string? EmailAddress { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? LoginName { get; set; }
        public int? LoginID { get; set; }
        public string? jwtToken { get; set; }
        public bool? IsAdmin { get; set; }

    }
}
