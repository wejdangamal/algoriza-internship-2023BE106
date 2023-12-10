using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vzeeta.Core.Model;
using Vzeeta.Core.Service;
using Vzeeta.Core.ViewModels;

namespace VzeetaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IRegistrationService userService;

        public AccountController(IRegistrationService userService)
        {
            this.userService = userService;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Doctor/SignUp")]
        public async Task<IActionResult> SignUpDoctor([FromForm] DoctorRegistrationDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.DoctorRegisterAsync(model);
                if (result)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            return BadRequest(new { Errors = ModelInValid() });
        }
        [Authorize(Roles ="Patient")]
        [HttpPost("User/SignUp")]
        public async Task<IActionResult> SignUpUser([FromForm] UserRegistrationVM model)
        {
            if (ModelState.IsValid)
            {

                var result = await userService.UserRegisterAsync(model);
                if (result)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            return BadRequest(new { Errors = ModelInValid() });
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> login(SignInVM model)
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
