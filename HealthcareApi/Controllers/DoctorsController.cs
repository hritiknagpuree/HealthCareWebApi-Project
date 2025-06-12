using HealthcareApi.Interfaces;
using HealthcareApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HealthcareApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _service;

        public DoctorsController(IDoctorService service)
        {
            _service = service;
        }

        // GET: api/doctors
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                if (!DoctorSession.IsDoctorLoggedIn)
                    return Unauthorized("Doctor not logged in");

                var result = await _service.GetAllDoctors();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // GET: api/doctors/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                if (!DoctorSession.IsDoctorLoggedIn)
                    return Unauthorized("Doctor not logged in");

                var result = await _service.GetDoctorById(id);
                if (result == null)
                    return NotFound("Doctor not found");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // PUT: api/doctors/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] DoctorUpdateDto dto)
        {
            try
            {
                if (!DoctorSession.IsDoctorLoggedIn)
                    return Unauthorized("Doctor not logged in");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var updated = await _service.UpdateDoctor(id, dto);
                if (!updated)
                    return NotFound("Doctor not found");

                return Ok("Doctor updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // DELETE: api/doctors/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (!DoctorSession.IsDoctorLoggedIn)
                    return Unauthorized("Doctor not logged in");

                var deleted = await _service.DeleteDoctor(id);
                if (!deleted)
                    return NotFound("Doctor not found");

                return Ok("Doctor deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
