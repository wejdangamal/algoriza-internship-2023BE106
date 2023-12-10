using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vzeeta.Core.DTOs;
using Vzeeta.Services.Interfaces.IAdmin;

namespace VzeetaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class AdminDoctorBoardController : ControllerBase
    {
        private readonly IAdminDoctorBoard board;

        public AdminDoctorBoardController(IAdminDoctorBoard board)
        {
            this.board = board;
        }
        [HttpGet("Doctors/{page}/{pageSize}")]
        public IActionResult Get(int page, int pageSize, [FromQuery] string search)
        {
            // [HINT] if wants to search by full DateOfBirth just test using this Format => dd/mm/yyyy ex:21/12/2001
            var _result = board.GetAll(page, pageSize, x => (x.ApplicationUsers.UserName.Contains(search) ||
                x.ApplicationUsers.gender.ToString() == search.ToUpper() ||
                x.ApplicationUsers.Email.Contains(search) ||
                x.ApplicationUsers.DateOfBirth.ToString().Contains(search) ||
                x.ApplicationUsers.PhoneNumber == search ||
                x.ApplicationUsers.firstName.Contains(search) ||
                x.ApplicationUsers.lastName.Contains(search) ||
                x.specializations.specializeType.Contains(search)),
                new string[] { "ApplicationUsers", "specializations" }).ToList();
            return Ok(_result);
        }
        [HttpGet("Doctor/{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var _result = await board.GetById(Id);
            if (_result != null)
                return Ok(_result);
            return NotFound("Not Found");
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Update([FromForm] DoctorUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var _result = await board.Update(model);
                    if (_result)
                        return Ok("Update Successfully");
                    else
                    {
                        return BadRequest(_result);
                    }
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }
            return BadRequest();

        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await board.Delete(id);
                if (result)
                    return Ok(result);
                else
                    return NotFound("Not Found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
