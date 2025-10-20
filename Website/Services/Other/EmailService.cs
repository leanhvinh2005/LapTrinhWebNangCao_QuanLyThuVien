using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using Website.Areas.User.Controllers;
using Website.Data;
using Website.Models;

namespace Website.Services.Other
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public interface IEmailService
    {
        Task SendEmail(string toEmail, string subject, string message);
    }

    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }

        public async Task SendEmail(string toEmail, string subject, string message)
        {
            var mail = new MailMessage()
            {
                From = new MailAddress(_settings.SenderEmail, _settings.SenderName),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            mail.To.Add(toEmail);

            using (var smtp = new SmtpClient(_settings.SmtpServer, _settings.SmtpPort))
            {
                smtp.Credentials = new NetworkCredential(_settings.Username, _settings.Password);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            }
        }
    }
}
