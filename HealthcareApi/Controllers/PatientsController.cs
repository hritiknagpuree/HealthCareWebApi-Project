using HealthcareApi.DTOs;
using HealthcareApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace HealthcareApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        // GET: api/patients
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                if (!PatientSession.IsPatientLoggedIn)
                    return Unauthorized("Patient not logged in");

                var patients = await _patientService.GetAllPatients();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // GET: api/patients/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                if (!PatientSession.IsPatientLoggedIn)
                    return Unauthorized("Patient not logged in");

                var patient = await _patientService.GetPatientById(id);
                if (patient == null)
                    return NotFound("Patient not found");

                return Ok(patient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        //// POST: api/patients
        //[HttpPost]
        //public async Task<IActionResult> Register([FromBody] PatientRegisterDto dto)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);

        //        var result = await _patientService.RegisterPatient(dto);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error: {ex.Message}");
        //    }
        //}

        // PUT: api/patients/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PatientUpdateDto dto)
        {
            try
            {
                if (!PatientSession.IsPatientLoggedIn)
                    return Unauthorized("Patient not logged in");

                var updated = await _patientService.UpdatePatient(id, dto);
                if (!updated)
                    return NotFound("Patient not found");

                return Ok("Patient updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // DELETE: api/patients/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (!PatientSession.IsPatientLoggedIn)
                    return Unauthorized("Patient not logged in");

                var deleted = await _patientService.DeletePatient(id);
                if (!deleted)
                    return NotFound("Patient not found");

                return Ok("Patient deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
