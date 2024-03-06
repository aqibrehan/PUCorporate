using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUCorporate.Model.Model
{
    public class MailModel
    {
        public List<string> ToMailIds { get; set; } = new List<string>();
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public bool IsHtmlBody { get; set; }
        public List<string> Attachments { get; set; } = new List<string>();
    }
}
