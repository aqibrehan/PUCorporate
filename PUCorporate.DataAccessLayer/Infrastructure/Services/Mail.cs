using PUCorporate.DataAccessLayer.Services.Interfaces;
using System.Net.Mail;
using System.Net;
using PUCorporate.Model.Model;

using Microsoft.Extensions.Configuration;

namespace PUCorporateAPI.Services.Services
{
    public class Mail : IMail
    {
        private readonly IConfiguration _configuration;
        public Mail(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public async Task<string> Sendmail(MailModel model, string email, string password)
        {
            try
            {
                using MailMessage mail = new MailMessage();
                mail.From = new MailAddress(email)
       ;
                model.ToMailIds.ForEach(oMailId =>
                {
                    mail.To.Add(oMailId);
                });
                mail.Subject = model.Subject;
                mail.Body = model.Body;
                mail.IsBodyHtml = model.IsHtmlBody;
                model.Attachments.ForEach(x =>
                {
                    mail.Attachments.Add(new Attachment(x));
                });

                using SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(email, password);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail)
       ;
                return "Mail Sent Successfully!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> SendForgotPasswordEmail(MailModel model, string recipientemail, string resetLink)
        {
            try
            {
                using MailMessage mail = new MailMessage();
                mail.From = new MailAddress(_configuration["Credentials:EmailToSendMail"]!);
                mail.To.Add(recipientemail);
                mail.Subject = "Password Reset";
                mail.Body = $"Click the link below to reset your password:<br/><a href='{resetLink}'>{resetLink}</a>";
                mail.IsBodyHtml = true;

                using SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(_configuration["Credentials:EmailToSendMail"]!, _configuration["Credentials:Password"]!);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail)
       ;

                return "Forget password email sent successfully!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
