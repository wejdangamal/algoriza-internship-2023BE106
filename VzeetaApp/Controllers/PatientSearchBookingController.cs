using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vzeeta.Core.DTOs;
using Vzeeta.Services.Interfaces.IPatient;

namespace VzeetaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Patient")]
    public class PatientSearchBookingController : ControllerBase
    {
        private readonly IPatientSearchBookings bookingRepository;

        public PatientSearchBookingController(IPatientSearchBookings bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }
        [HttpGet("Doctors/Details/{page}/{pageSize}")]
        public IActionResult getAllDoctorInfo(int page,int pageSize,[FromQuery]string search)
        {
            var bookings = bookingRepository.GetAll(page,pageSize, x => (x.ApplicationUsers.UserName.Contains(search) ||
                x.ApplicationUsers.gender.ToString() == search.ToUpper() ||
                x.ApplicationUsers.Email.Contains(search) ||
                x.ApplicationUsers.DateOfBirth.ToString().Contains(search) ||
                x.ApplicationUsers.PhoneNumber == search ||
                x.ApplicationUsers.firstName.Contains(search) ||
                x.ApplicationUsers.lastName.Contains(search) ||
                x.specializations.specializeType.Contains(search)),
                new string[] { "ApplicationUsers", "specializations", "Appointments", "Appointments.times" }).ToList();
            if (bookings.Any())
            {
                return Ok(bookings);
            }
            return BadRequest("No Bookings Found!");
        }
        [HttpPost("Booking/{timeId}")]
        public async Task<IActionResult> add(int timeId,[FromBody]string? discountCode)
        {
            try
            {
                var res = await bookingRepository.Booking(timeId, discountCode);
                if (res)
                    return Ok("Time Booked Successfully");
                return BadRequest("Something Wrong!");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
         
        }
    }
}
