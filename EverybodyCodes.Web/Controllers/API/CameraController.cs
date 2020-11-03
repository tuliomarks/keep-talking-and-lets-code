using System;
using System.Linq;
using EverybodyCodes.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EverybodyCodes.Web.Controllers.API
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CameraController : ControllerBase
    {
        private ICameraService _service;

        public CameraController(ICameraService service)
        {
            this._service = service;
        }

        // GET: api/Camera
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(this._service.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Camera/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                return Ok(this._service.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
