using HealthcareApi.DTOs;
using HealthcareApi.ViewModels;

public interface IPatientService
{
    Task<string> RegisterPatient(PatientRegisterDto dto);
    Task<string> LoginPatient(LoginDto dto);
    Task<IEnumerable<PatientViewModel>> GetAllPatients();
    Task<PatientViewModel?> GetPatientById(Guid id);
    Task<bool> UpdatePatient(Guid id, PatientUpdateDto dto);
    Task<bool> DeletePatient(Guid id);
}
