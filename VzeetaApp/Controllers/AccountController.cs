using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vzeeta.Core.Service;
using Vzeeta.Core.ViewModels;
using Vzeeta.Services.Interfaces.IAdmin;

namespace VzeetaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IRegistrationService userService;
        private readonly ISendEmailService sendEmailService;

        public AccountController(IRegistrationService userService, ISendEmailService sendEmailService)
        {
            this.userService = userService;
            this.sendEmailService = sendEmailService;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Doctor/SignUp")]
        public async Task<IActionResult> SignUpDoctor([FromForm] DoctorRegistrationDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await userService.DoctorRegisterAsync(model);
                    if (result)
                    {
                        var body = $"<h1>Hi This is Your Account Info to SignIn and use Vzeeta App</h1>" +
                        $"Email: {model.email}<br> " +
                        $"Password: {model.password}<br> " + "Thanks For Using Vzeeta";
                        await sendEmailService.sendEmail(model.email, body);
                        return Ok(result);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(new { Errors = ModelInValid() });
        }
        [AllowAnonymous]
        [HttpPost("User/SignUp")]
        public async Task<IActionResult> SignUpUser([FromForm] UserRegistrationDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await userService.UserRegisterAsync(model);
                    if (result)
                        return Ok(result);
                }catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(new { Errors = ModelInValid() });
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> login(SignInDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.LoginAsync(model);
                if (result.Succeeded)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("SignIn failed. Please check your data and try again.");

                }
            }
            return BadRequest(new { Errors = ModelInValid() });
        }
        private Dictionary<string, List<string>> ModelInValid()
        {
            var errors = ModelState.ToDictionary(
               kvp => kvp.Key,
               kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
           );

            return errors;
        }
    }
}
