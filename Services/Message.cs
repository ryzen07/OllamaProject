using System.Net.Mail;
using System.Net;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using OllamaProject.Entities;
using Microsoft.Extensions.Options;

namespace OllamaProject.Services
{
    public class Message : IMessage
    {

        public GmailSettings _gmailSettings { get; }

        public Message(IOptions<GmailSettings> gmailSettings) 
        {
            _gmailSettings = gmailSettings.Value;
        }

     
        public void SendEmail ( string subject, string body, string to)
        {
            try
            {
                var fromEmail = _gmailSettings.Username;
                var password = _gmailSettings.Password;

                var message = new MailMessage();
                message.From = new MailAddress(fromEmail);
                message.Subject = subject;
                message.To.Add(new MailAddress(to));
                message.Body = body;
                message.IsBodyHtml = true;

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = _gmailSettings.Port,
                    Credentials = new NetworkCredential(fromEmail, password),
                    EnableSsl = true
                };
                smtpClient.Send(message);
            } catch(Exception e) 
            {
                throw new Exception("no se pudo enviar el correo", e);
            }
        }
    }
}