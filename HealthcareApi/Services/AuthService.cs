using HealthcareApi.Interfaces;

public class AuthService : IAuthService
{
    private readonly IDoctorRepository _doctorRepo;
    private readonly IPatientRepository _patientRepo;

    public AuthService(IDoctorRepository doctorRepo, IPatientRepository patientRepo)
    {
        _doctorRepo = doctorRepo;
        _patientRepo = patientRepo;
    }

    public async Task<string> RegisterDoctor(DoctorRegisterDto dto)
    {
        if (await _doctorRepo.DoctorExists(dto.Username))
            return "Username already taken";

        var doctor = new Doctor
        {
            FullName = dto.FullName,
            Specialty = dto.Specialty,
            Username = dto.Username,
            Password = dto.Password
        };

        await _doctorRepo.Register(doctor);
        return "Success";
    }

    public async Task<string> RegisterPatient(PatientRegisterDto dto)
    {
        if (await _patientRepo.PatientExists(dto.Username))
            return "Username already taken";

        var patient = new Patient
        {
            FullName = dto.FullName,
            Age = dto.Age,
            Username = dto.Username,
            Password = dto.Password
        };

        await _patientRepo.Register(patient);
        return "Success";
    }

    public async Task<string> LoginDoctor(LoginDto dto)
    {
        var user = await _doctorRepo.Login(dto.Username, dto.Password);
        return user != null ? "Success" : "Invalid doctor credentials";
    }

    public async Task<string> LoginPatient(LoginDto dto)
    {
        var user = await _patientRepo.Login(dto.Username, dto.Password);
        return user != null ? "Success" : "Invalid patient credentials";
    }
}
