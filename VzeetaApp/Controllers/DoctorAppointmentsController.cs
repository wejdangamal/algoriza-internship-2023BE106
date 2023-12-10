using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vzeeta.Core.DTOs;
using Vzeeta.Core.Model;
using Vzeeta.Services.Interfaces.IDoctor;

namespace VzeetaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class DoctorAppointmentsController : ControllerBase
    {
        private readonly IDoctorRepository doctorRepository;

        public DoctorAppointmentsController(IDoctorRepository doctorRepository)
        {
            this.doctorRepository = doctorRepository;
        }
        [HttpPost("Appointments")]
        public async Task<IActionResult> addAppointments([FromBody] AppointmentsDTO model)
        {
            if (ModelState.IsValid)
            {

                model.times = model.times.ToDictionary(
                      kvp => kvp.Key,
                      kvp => kvp.Value.DistinctBy(x => x.time).ToList() // to make time added once and UNIQUE
                  );

                var res = await doctorRepository.Add(model);
                if (res)
                    return Ok(res);
            }
            return BadRequest(ModelState);
        }
        [HttpPut("TimeEdit")]
        public async Task<IActionResult> editAppointment([FromBody] TimeDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var res = await doctorRepository.Update(model);
                    if(res)
                    return Ok("Update Successfully");
                }catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }


            }
            return BadRequest("OOPs");
        }
        [HttpDelete("RemoveTime/{Id}")]
        public async Task<IActionResult> deleteAppointment([FromRoute]int Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var res = await doctorRepository.Delete(Id);
                    if (res)
                        return Ok("Delete Successfully");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }


            }
            return BadRequest("OOPs");
        }
    }
}
