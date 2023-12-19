using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VzeetaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhatController : ControllerBase
    {
        [Authorize(Roles = "Patient")]
        [HttpGet]
        public IActionResult get()
        {
            return Ok("ooooooki");
        }
    }
}
