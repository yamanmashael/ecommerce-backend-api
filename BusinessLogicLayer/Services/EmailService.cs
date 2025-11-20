using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public  class EmailService
    {


        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public async Task SendEmail(string receptor, string subject, string body)
        {
            var email = configuration.GetSection("EMAIL_CONFIGURATION:EMAIL").Value;
            var password = configuration.GetSection("EMAIL_CONFIGURATION:PASSWORD").Value;
            var host = configuration.GetSection("EMAIL_CONFIGURATION:HOST").Value;
            var port = int.Parse(configuration.GetSection("EMAIL_CONFIGURATION:PORT").Value!);

            var smtpClient = new SmtpClient(host, port);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(email, password);

            var massage = new MailMessage(email!, receptor, subject, body);
            await smtpClient.SendMailAsync(massage);


        }

    }
}
