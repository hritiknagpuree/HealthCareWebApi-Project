using HealthcareApi.ViewModels;

namespace HealthcareApi.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterDoctor(DoctorRegisterDto dto);
        Task<string> RegisterPatient(PatientRegisterDto dto);
        Task<string> LoginDoctor(LoginDto dto);
        Task<string> LoginPatient(LoginDto dto);
    }
}
