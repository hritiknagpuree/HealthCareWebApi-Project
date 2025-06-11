using HealthcareApi.ViewModels;

namespace HealthcareApi.Interfaces
{
    public interface IPatientService
    {
        Task<string> RegisterPatient(PatientRegisterDto dto);
        Task<string> LoginPatient(LoginDto dto);
        Task<IEnumerable<PatientViewModel>> GetAllPatients();
    }
}
