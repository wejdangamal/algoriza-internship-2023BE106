using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vzeeta.Services.Interfaces.IAdmin;

namespace VzeetaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ISendEmailService sendEmailService;

        public TestController(IHttpContextAccessor httpContextAccessor,ISendEmailService sendEmailService)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.sendEmailService = sendEmailService;
        }
        [HttpGet("UserId")]
        public  IActionResult GetId()
        {
            var userId =  httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(userId);
        }
        [HttpPost("send")]
        public async Task<IActionResult> Post()
        {
            await sendEmailService.sendEmail("wg63wegdan92@gmail.com", "hi");
            return Ok();
        }
    }
}
