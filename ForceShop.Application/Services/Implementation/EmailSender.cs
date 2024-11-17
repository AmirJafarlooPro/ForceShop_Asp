using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ForceShop.Application.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace ForceShop.Application.Services.Implementation
{
    public class EmailSender:IEmailSender
    {

        #region ctor

        private readonly ILogger<EmailSender> _logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            _logger = logger;
        }

        #endregion

        #region SendEmail

        public async Task<bool> SendEmailAsync(string to, string subject, string body)
        {
            try
            {

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("forceshop.pro@gmail.com", "کد تایید فراموشی کلمه عبور");
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpServer.Port = 587;
                SmtpServer.EnableSsl = true;

                SmtpServer.Credentials = new System.Net.NetworkCredential("forceshop.pro@gmail.com", "yior wgpe uzji gitb");
                SmtpServer.Send(mail);

                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError($"Email Error\n\tErrorMessage:: {exception.Message}");
                return false;
            }
        }

        #endregion


    }
}
