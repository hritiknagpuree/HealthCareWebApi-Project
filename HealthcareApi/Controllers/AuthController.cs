using HealthcareApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IDoctorRepository _doctorRepo;
    private readonly IPatientRepository _patientRepo;

    // Constructor injection for doctor and patient repositories
    public AuthController(IDoctorRepository doctorRepo, IPatientRepository patientRepo)
    {
        _doctorRepo = doctorRepo;
        _patientRepo = patientRepo;
    }

    // Register a new doctor
    [HttpPost("register-doctor")]
    public async Task<IActionResult> RegisterDoctor(DoctorRegisterDto dto)
    {
        // Check if the username already exists
        if (await _doctorRepo.DoctorExists(dto.Username))
            return BadRequest("Username already taken");

        // Create a new doctor object from the DTO
        var doctor = new Doctor
        {
            FullName = dto.FullName,
            Specialty = dto.Specialty,
            Username = dto.Username,
            Password = dto.Password
        };

        // Save the doctor to the database
        await _doctorRepo.Register(doctor);
        return Ok("Doctor registered successfully.");
    }

    // Register a new patient
    [HttpPost("register-patient")]
    public async Task<IActionResult> RegisterPatient(PatientRegisterDto dto)
    {
        // Check if the username already exists
        if (await _patientRepo.PatientExists(dto.Username))
            return BadRequest("Username already taken");

        // Create a new patient object from the DTO
        var patient = new Patient
        {
            FullName = dto.FullName,
            Age = dto.Age,
            Username = dto.Username,
            Password = dto.Password
        };

        // Save the patient to the database
        await _patientRepo.Register(patient);
        return Ok("Patient registered successfully.");
    }

    // Login as a doctor
    [HttpPost("login-doctor")]
    public async Task<IActionResult> LoginDoctor(LoginDto dto)
    {
        // Try to find doctor with given credentials
        var user = await _doctorRepo.Login(dto.Username, dto.Password);
        if (user == null)
            return Unauthorized("Invalid doctor credentials");

        return Ok("Doctor logged in.");
    }

    // Login as a patient
    [HttpPost("login-patient")]
    public async Task<IActionResult> LoginPatient(LoginDto dto)
    {
        // Try to find patient with given credentials
        var user = await _patientRepo.Login(dto.Username, dto.Password);
        if (user == null)
            return Unauthorized("Invalid patient credentials");

        return Ok("Patient logged in.");
    }
}
