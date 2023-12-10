using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vzeeta.Core.Model;
using Vzeeta.Core.Model.Enums;
using Vzeeta.Services.Interfaces.IDoctorInterfaces;

namespace VzeetaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Doctor")]
    public class DoctorRequestsServiceController : ControllerBase
    {
        private readonly IDoctorRequests doctorRequests;
        private readonly IHttpContextAccessor currentClientId;

        public DoctorRequestsServiceController(IDoctorRequests doctorRequests, IHttpContextAccessor currentClientId)
        {
            this.doctorRequests = doctorRequests;
            this.currentClientId = currentClientId;
        }
        [HttpPut("ConfirmCheckUp/{id}")]
        public async Task<IActionResult> confirmBooking(int id)
        {
            try
            {
                var result = await doctorRequests.confirmCheckUp(id);
                if (result)
                {
                    return Ok("Confirmation Done Successfully");
                }
                return BadRequest("SomeThing Wrong");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("Bookings/{page}/{pageSize}")]
        public IActionResult getAll(int page,int pageSize,[FromQuery]Day date)
        {
            var currentDoctorId = currentClientId.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var requests = doctorRequests.GetAllRequests(page,pageSize,
                x=>x.TimeSlot.appointments.Day==date&&x.TimeSlot.appointments.doctorId== currentDoctorId,
                new string[] { "TimeSlot", "Patient", "TimeSlot.appointments" })
                .ToList();
            if(requests != null)
            {
                return Ok(requests);
            }
            return BadRequest("Not Found!");
        }

    }
}
