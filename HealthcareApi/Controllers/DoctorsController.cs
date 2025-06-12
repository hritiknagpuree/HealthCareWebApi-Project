using HealthcareApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorService _service;

    public DoctorsController(IDoctorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        if (!DoctorSession.IsDoctorLoggedIn)
            return Unauthorized("Doctor not logged in");

        var doctors = await _service.GetAllDoctors();
        return Ok(doctors);
    }
}
