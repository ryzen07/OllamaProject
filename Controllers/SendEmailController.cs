using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OllamaProject.DTO;
using OllamaProject.Entities;
using OllamaProject.Services;

namespace OllamaProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendEmailController : ControllerBase
    {
            private readonly IMessage _emailService;
            public SendEmailController(IMessage emailService)
            {
                this._emailService = emailService;
            }

            [HttpPost]
            [Route("EnviarMail")]
            public IActionResult SendEmail(SendEmailRequest request)
            {
                _emailService.SendEmail(request.Subject, request.Body,request.to);
                return Ok();
            }
    }
}
