using Business.Abstract;
using Entity.Dtos.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : BaseController
    {
        private readonly IEmailSender _emailSender;

        public EmailsController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] RequestEmailDto dto)
        {
            try
            {
                foreach (var mail in dto.To)
                {
                    await _emailSender.SendEmailAsync(mail, dto.Subject, dto.Body);
                }
                return Ok("Başarıyla gönderildi");


            }
            catch (Exception e)
            {
                throw e;
            }   
        }
    }
}
