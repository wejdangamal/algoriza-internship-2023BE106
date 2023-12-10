using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vzeeta.Services.Interfaces.IPatient;

namespace VzeetaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Patient")]
    public class PatientBookingsController : ControllerBase
    {
        private readonly IPatientAllBookings allBookings;

        public PatientBookingsController(IPatientAllBookings allBookings)
        {
            this.allBookings = allBookings;
        }
        [HttpGet("Bookings")]
        public IActionResult getAllPatientBookings()
        {
            var bookings = allBookings.GetAllBookings().ToList();
            if(bookings.Any())
            {
                return Ok(bookings);
            }
            return BadRequest("No Bookings Found!");
        }
    }
}
