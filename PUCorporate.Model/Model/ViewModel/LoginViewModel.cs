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
      
        public string? Email { get; set; }
        public string? Token { get; set; }
     
        public string? UserId { get; set; }
    }
}
