using Microsoft.AspNetCore.Mvc;
using HealthcareApi.Interfaces;

namespace HealthcareApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepository _patientRepo;

        public PatientsController(IPatientRepository patientRepo)
        {
            _patientRepo = patientRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patients = await _patientRepo.GetAllPatients();
            return Ok(patients);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Patient patient)
        {
            var added = await _patientRepo.AddPatient(patient);
            return Ok(added);
        }
    }
}
