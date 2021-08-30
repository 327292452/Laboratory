using System;
using System.Collections.Generic;
using System.Text;

namespace MyLibrary.SUPTools.EMail
{
    public class EMailModel
    {
    }

    public class ConfigHost
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
    }

    public class ConfigMail
    {
        public string From { get; set; }
        public string[] To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string[] Attachments { get; set; }
        public string[] Resources { get; set; }
    }
}
