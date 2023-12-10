
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vzeeta.Core.Model;
using Vzeeta.Core.Model.Enums;
using Vzeeta.Services.Interfaces.IAdmin;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace VzeetaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminPatientsBoardController : ControllerBase
    {
        private IAdminPatientsBoard _board;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminPatientsBoardController(IAdminPatientsBoard board, UserManager<ApplicationUser> userManager)
        {
            _board = board;
            this.userManager = userManager;
        }

        [HttpGet("Patients/{page}/{pageSize}")]
        public IActionResult GetAll([FromRoute] int page, [FromRoute] int pageSize, [FromQuery] string search)
        {

            var _results = _board.GetAll(page, pageSize,
                x => (
                x.Role == UserRole.Patient && (
                x.UserName.Contains(search) ||
                x.gender.ToString() == search.ToUpper() ||
                x.Email.Contains(search) ||
                x.DateOfBirth.ToString().Contains(search) ||
                x.PhoneNumber == search ||
                x.firstName.Contains(search) ||
                x.lastName.Contains(search)
                ))).ToList();
            return Ok(_results);
        }
        [HttpGet("Patient/{id}")]
        public async Task<IActionResult> Get(string id)
        {

            try
            {
                var _result = await _board.GetById(id);
                return Ok(_result);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}
