namespace HealthcareApi.Interfaces
{
    // Patient data operations interface
    public interface IPatientRepository
    {
        // Check if patient username exists
        Task<bool> PatientExists(string username);

        // Register new patient
        Task Register(Patient patient);

        // Patient login
        Task<Patient?> Login(string username, string password);

        // Get all patients
        Task<IEnumerable<Patient>> GetAllPatients();

        // Add a patient (returning created patient)
        Task<Patient> AddPatient(Patient patient);

        // Get patient by username
        Task<Patient> GetByUsername(string username);
    }
}
