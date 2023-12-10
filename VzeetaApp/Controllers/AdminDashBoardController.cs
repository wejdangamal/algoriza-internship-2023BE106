using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vzeeta.Services.Interfaces.IAdmin;

namespace VzeetaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class AdminDashBoardController : ControllerBase
    {
        private readonly IAdminDashBoard _adminDashBoard;

        public AdminDashBoardController(IAdminDashBoard adminDashBoard)
        {
            _adminDashBoard = adminDashBoard;
        }
        [HttpGet("NumberOfDoctors")]
        public IActionResult doctorsCount()
        {
            var result = _adminDashBoard.NumOfDoctors();
            return Ok($"Number Of Doctors : {result}");
        }
        [HttpGet("NumberOfPatients")]
        public IActionResult patientsCount()
        {
            var result = _adminDashBoard.NumOfPatients();
            return Ok($"Number Of Patients : {result}");
        }
        [HttpGet("Resquests")]
        public IActionResult requestsCount()
        {
            var result = _adminDashBoard.NumOfRequests();                  
                    return Ok(result);           
        }
        [HttpGet("TopFiveSpecializations")]
        public IActionResult topSpecializations()
        {
            var result = _adminDashBoard.TopFiveSpecializations();
            return Ok(result);
        }
        [HttpGet("TopTenDoctors")]
        public IActionResult topDoctors()
        {
            var result = _adminDashBoard.TopTenDoctors();
            return Ok(result);
        }

    }
}
