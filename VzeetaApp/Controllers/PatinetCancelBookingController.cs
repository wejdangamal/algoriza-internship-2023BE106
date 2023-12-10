using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vzeeta.Services.Interfaces.IPatient;
namespace VzeetaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Patient")]
    public class PatinetCancelBookingController : ControllerBase
    {
        private readonly ICancelBooking cancelBooking;

        public PatinetCancelBookingController(ICancelBooking cancelBooking)
        {
            this.cancelBooking = cancelBooking;
        }
        [HttpPut("CancelBooking/{id}")]
        public async Task<IActionResult> cancel(int id)
        {
            try
            {
                var result = await cancelBooking.CancelBookingAsync(id);
                if (result)
                {
                    return Ok("Cancellation Done Successfully");
                }
                return BadRequest("SomeThing Wrong");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
