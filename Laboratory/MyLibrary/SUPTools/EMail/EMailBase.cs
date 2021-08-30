using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace MyLibrary.SUPTools.EMail
{
    public class EMailBase : IEMailBase
    {
        private MailMessage mailMsg { get; set; }
        private SmtpClient hostSmtp { get; set; }
        public void CreateHost(ConfigHost host)
        {
            if (string.IsNullOrEmpty(host.Server)) throw new Exception("Server is null!");
            if (host.Port==0) throw new Exception("Port not is 0!");
            if (string.IsNullOrEmpty(host.Username)) throw new Exception("Username is null!");

            try
            {
                hostSmtp = new SmtpClient(host.Server, host.Port);
                hostSmtp.Credentials = new System.Net.NetworkCredential(host.Username, host.Password);
                hostSmtp.EnableSsl = host.EnableSsl;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CreateMail(ConfigMail mail)
        {
            mailMsg = new MailMessage();
            mailMsg.From = new MailAddress(mail.From);

            foreach (var t in mail.To)
                mailMsg.To.Add(t);

            mailMsg.Subject = mail.Subject;
            mailMsg.Body = mail.Body;
            mailMsg.IsBodyHtml = true;
            mailMsg.BodyEncoding = System.Text.Encoding.UTF8;
        }

        public void CreateMultiMail(ConfigMail mail)
        {
            CreateMail(mail);

            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString("If you see this message, it means that your mail client does not support html.", Encoding.UTF8, "text/plain"));

            var html = AlternateView.CreateAlternateViewFromString(mail.Body, Encoding.UTF8, "text/html");
            foreach (string resource in mail.Resources)
            {
                var image = new LinkedResource(resource, "image/jpeg");
                image.ContentId = Convert.ToBase64String(Encoding.Default.GetBytes(Path.GetFileName(resource)));
                html.LinkedResources.Add(image);
            }
            mailMsg.AlternateViews.Add(html);

            foreach (var attachment in mail.Attachments)
            {
                mailMsg.Attachments.Add(new Attachment(attachment));
            }
        }

        public void SendMail()
        {
            if (hostSmtp != null && mailMsg != null)
                hostSmtp.Send(mailMsg);
            else
                throw new Exception("These is not a host to send mail or there is not a mail need to be sent.");
        }
    }
}
