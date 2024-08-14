using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
namespace Users.Application.Interfaces
{

    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }

    public class EmailService : IEmailService
    {
        public EmailService(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Inno_Shop", Configuration["Mail:Email"]));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync(Configuration["Mail:Email"], Configuration["Mail:Password"]);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }

}
