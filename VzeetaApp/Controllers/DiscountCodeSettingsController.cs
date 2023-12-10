using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vzeeta.Core.Model;
using Vzeeta.Core.ViewModels;
using Vzeeta.Services.Interfaces.IAdmin;

namespace VzeetaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DiscountCodeSettingsController : ControllerBase
    {
        private readonly IAdminSetting setting;

        public DiscountCodeSettingsController(IAdminSetting setting)
        {
            this.setting = setting;
        }
        [HttpPost("Code")]
        public async Task<IActionResult> addCode([FromBody] DiscountCodeVM code)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var IsAdded = await setting.Add(code);
                    if (IsAdded)
                    {
                        return Ok("Is Added Successfully");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            return BadRequest(new { Error = ModelInValid() });
        }
        [HttpPut("Deactivate/{id}")]
        public async Task<IActionResult> deactivateCode(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var IsDeactivate = await setting.Deactivate(id);
                    if (IsDeactivate)
                    {
                        return Ok("Deactivate Successfully");
                    }
                    else
                    {
                        return NotFound(IsDeactivate);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest(new { Error = ModelInValid() });
            }
        }
        [HttpPut("Edit")]
        public async Task<IActionResult> editCode(DiscountCode_Coupon model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var IsUpdated = await setting.Update(model);
                    if (IsUpdated)
                    {
                        return Ok("Updated Successfully");
                    }
                    else
                        return BadRequest(IsUpdated);
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }
            else
            {
                return BadRequest(new { Error = ModelInValid() });
            }
        }
        [HttpDelete("DeleteCode/{id}")]
        public async Task<IActionResult> deleteCode(int id)
        {
            if (ModelState.IsValid)
            {
                var IsDeleted = await setting.Delete(id);
                if (IsDeleted)
                {
                    return Ok("Deleted Successfully");
                }
                else
                {
                    return NotFound(IsDeleted);
                }
            }
            else
            {
                return BadRequest(new { Error = ModelInValid() });
            }
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
