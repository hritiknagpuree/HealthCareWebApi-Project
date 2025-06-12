using HealthcareApi.Interfaces;
using HealthcareApi.Services;
using HealthcareApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService; // Inject auth service
    }

    [HttpPost("register-doctor")]
    public async Task<IActionResult> RegisterDoctor(DoctorRegisterDto dto)
    {
        try
        {
            var result = await _authService.RegisterDoctor(dto); // Register doctor
            if (result != "Success")
                return BadRequest(result); // Return error if registration fails

            return Ok("Doctor registered successfully."); 
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}"); // Handle unexpected error
        }
    }

    [HttpPost("register-patient")]
    public async Task<IActionResult> RegisterPatient(PatientRegisterDto dto)
    {
        try
        {
            var result = await _authService.RegisterPatient(dto); // Register patient
            if (result != "Success")
                return BadRequest(result); // Return error if registration fails

            return Ok("Patient registered successfully."); // Return success response
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}"); 
        }
    }

    [HttpPost("login-doctor")]
    public async Task<IActionResult> LoginDoctor(LoginDto dto)
    {
        try
        {
            var result = await _authService.LoginDoctor(dto); // Doctor login
            if (result != "Success")
                return Unauthorized(result); // Return unauthorized if login fails

            DoctorSession.IsDoctorLoggedIn = true; // Set doctor session
            return Ok("Doctor logged in."); 
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}"); 
        }
    }

    [HttpPost("login-patient")]
    public async Task<IActionResult> LoginPatient(LoginDto dto)
    {
        try
        {
            var result = await _authService.LoginPatient(dto); // Patient login
            if (result != "Success")
                return Unauthorized(result); 

            PatientSession.IsPatientLoggedIn = true; // Set patient session
            return Ok("Patient logged in."); 
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}
