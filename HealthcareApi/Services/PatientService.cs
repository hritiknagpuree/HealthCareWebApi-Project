using HealthcareApi.Interfaces;
using HealthcareApi.ViewModels;

namespace HealthcareApi.Services
{
    // Service class for handling patient-related operations
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repo;

        // Inject the patient repository
        public PatientService(IPatientRepository repo)
        {
            _repo = repo;
        }

        // Register a new patient
        public async Task<string> RegisterPatient(PatientRegisterDto dto)
        {
            // Check if username is already taken
            if (await _repo.PatientExists(dto.Username))
                return "Patient username already exists.";

            // Create a new patient object
            var patient = new Patient
            {
                FullName = dto.FullName,
                Age = dto.Age,
                Username = dto.Username,
                Password = dto.Password
            };

            // Save to database
            await _repo.Register(patient);
            return "Patient registered successfully.";
        }

        // Login patient with credentials
        public async Task<string> LoginPatient(LoginDto dto)
        {
            var patient = await _repo.Login(dto.Username, dto.Password);
            return patient != null ? "Patient login successful." : "Invalid credentials.";
        }

        // Get all patients
        public async Task<IEnumerable<PatientViewModel>> GetAllPatients()
        {
            var patients = await _repo.GetAllPatients();

            // Map to view model list
            return patients.Select(p => new PatientViewModel
            {
                Id = p.Id,
                FullName = p.FullName,
                Age = p.Age
            });
        }
    }
}
