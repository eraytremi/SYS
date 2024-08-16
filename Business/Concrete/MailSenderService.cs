using Business.Abstract;
using Entity.Dtos.Email;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace Business.Concrete
{
    public class MailSenderService : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly EmailGetDto _emailGetDto;
        public MailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
            _emailGetDto = new EmailGetDto
            {
                SenderMail = _configuration.GetSection("EmailConfig").GetValue<string>("Mail"),
                SenderPass = _configuration.GetSection("EmailConfig").GetValue<string>("Password"),
                EnableSsl = true,
                Host = _configuration.GetSection("EmailConfig").GetValue<string>("Host"),
                Port = _configuration.GetSection("EmailConfig").GetValue<int>("Port")
            };
        }


        public async Task SendEmailAsync(string email, string subject, string body)
        {

            using (SmtpClient client = new SmtpClient(_emailGetDto.Host))
            {
                client.Port = _emailGetDto.Port;
                client.EnableSsl = _emailGetDto.EnableSsl;

                NetworkCredential smtpUser = new NetworkCredential(_emailGetDto.SenderMail, _emailGetDto.SenderPass);
                client.UseDefaultCredentials = false;
                client.Credentials = smtpUser;

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(_emailGetDto.SenderMail);
                mail.To.Add(email);
                mail.Subject = subject;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.Body = body;
                mail.IsBodyHtml = true;
                mail.BodyEncoding = Encoding.UTF8;
                await client.SendMailAsync(mail);

            }

        }
    }
}
