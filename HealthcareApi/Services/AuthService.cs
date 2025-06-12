using HealthcareApi.Interfaces;

/// <summary>
/// Handles user authentication and registration
/// </summary>
public class AuthService : IAuthService
{
    private readonly IDoctorRepository _doctorRepo;
    private readonly IPatientRepository _patientRepo;

    public AuthService(IDoctorRepository doctorRepo, IPatientRepository patientRepo)
    {
        _doctorRepo = doctorRepo;
        _patientRepo = patientRepo;
    }

    /// <summary>
    /// Registers a new doctor
    /// </summary>
    public async Task<string> RegisterDoctor(DoctorRegisterDto dto)
    {
        // Check if username exists
        if (await _doctorRepo.DoctorExists(dto.Username))
            return "Username already taken";

        // Create doctor entity
        var doctor = new Doctor
        {
            FullName = dto.FullName,
            Specialty = dto.Specialty,
            Username = dto.Username,
            Password = dto.Password
        };

        // Save to database
        await _doctorRepo.Register(doctor);
        return "Success";
    }

    /// <summary>
    /// Registers a new patient
    /// </summary>
    public async Task<string> RegisterPatient(PatientRegisterDto dto)
    {
        // Check if username exists
        if (await _patientRepo.PatientExists(dto.Username))
            return "Username already taken";

        // Create patient entity
        var patient = new Patient
        {
            FullName = dto.FullName,
            Age = dto.Age,
            Username = dto.Username,
            Password = dto.Password
        };

        // Save to database
        await _patientRepo.Register(patient);
        return "Success";
    }

    /// <summary>
    /// Authenticates doctor login
    /// </summary>
    public async Task<string> LoginDoctor(LoginDto dto)
    {
        // Validate credentials
        var user = await _doctorRepo.Login(dto.Username, dto.Password);
        return user != null ? "Success" : "Invalid doctor credentials";
    }

    /// <summary>
    /// Authenticates patient login
    /// </summary>
    public async Task<string> LoginPatient(LoginDto dto)
    {
        // Validate credentials
        var user = await _patientRepo.Login(dto.Username, dto.Password);
        return user != null ? "Success" : "Invalid patient credentials";
    }
}