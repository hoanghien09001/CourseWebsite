using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Handle.HandleEmail
{
    public class EmailConfiguration
    {
        public string From { get; set; } = string.Empty;
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
