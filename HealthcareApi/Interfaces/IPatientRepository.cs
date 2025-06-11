
namespace HealthcareApi.Interfaces
{
    public interface IPatientRepository
    {
        Task<bool> PatientExists(string username);
        Task Register(Patient patient);
        Task<Patient?> Login(string username, string password);
        Task<IEnumerable<Patient>> GetAllPatients();
        Task<Patient> AddPatient(Patient patient);
        Task<Patient> GetByUsername(string username);


    }
}
