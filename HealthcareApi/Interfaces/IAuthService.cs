using HealthcareApi.ViewModels;

namespace HealthcareApi.Interfaces
{
    // Auth service interface
    public interface IAuthService
    {
        // Register doctor
        Task<string> RegisterDoctor(DoctorRegisterDto dto);

        // Register patient
        Task<string> RegisterPatient(PatientRegisterDto dto);

        // Doctor login
        Task<string> LoginDoctor(LoginDto dto);

        // Patient login
        Task<string> LoginPatient(LoginDto dto);
    }
}
