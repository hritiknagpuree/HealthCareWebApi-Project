using HealthcareApi.DTOs;
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
            if (await _repo.PatientExists(dto.Username))
                return "Patient username already exists.";

            var patient = new Patient
            {
                FullName = dto.FullName,
                Age = dto.Age,
                Username = dto.Username,
                Password = dto.Password // Note: use hashing in production
            };

            await _repo.Register(patient);
            return "Patient registered successfully.";
        }

        // Login patient
        public async Task<string> LoginPatient(LoginDto dto)
        {
            var patient = await _repo.Login(dto.Username, dto.Password);
            if (patient != null)
            {
                PatientSession.IsPatientLoggedIn = true;
                return "Patient login successful.";
            }

            return "Invalid credentials.";
        }

        // Get all patients
        public async Task<IEnumerable<PatientViewModel>> GetAllPatients()
        {
            var patients = await _repo.GetAllPatients();
            return patients.Select(p => new PatientViewModel
            {
                Id = p.Id,
                FullName = p.FullName,
                Age = p.Age
            });
        }

        // Get patient by ID
        public async Task<PatientViewModel?> GetPatientById(Guid id)
        {
            var patient = await _repo.GetPatientById(id);
            if (patient == null) return null;

            return new PatientViewModel
            {
                Id = patient.Id,
                FullName = patient.FullName,
                Age = patient.Age
            };
        }

        // Update patient
        public async Task<bool> UpdatePatient(Guid id, PatientUpdateDto dto)
        {
            var patient = await _repo.GetPatientById(id);
            if (patient == null) return false;

            patient.FullName = dto.FullName;
            patient.Age = dto.Age;
            patient.Password = dto.Password;

            return await _repo.UpdatePatient(patient);
        }

        // Delete patient
        public async Task<bool> DeletePatient(Guid id)
        {
            var patient = await _repo.GetPatientById(id);
            if (patient == null) return false;

            return await _repo.DeletePatient(patient);
        }
    }
}
