using HealthcareApi.ViewModels;

namespace HealthcareApi.Interfaces
{
    // Patient service interface
    public interface IPatientService
    {
        // Register new patient
        Task<string> RegisterPatient(PatientRegisterDto dto);

        // Patient login
        Task<string> LoginPatient(LoginDto dto);

        // Get all patients as view models
        Task<IEnumerable<PatientViewModel>> GetAllPatients();
    }
}
