using HealthcareApi.Interfaces;
using HealthcareApi.ViewModels;

namespace HealthcareApi.Services
{
    // AuthService handles registration and login for doctors and patients
    public class AuthService : IAuthService
    {
        private readonly IDoctorRepository _doctorRepo;
        private readonly IPatientRepository _patientRepo;
        public AuthService(IDoctorRepository doctorRepo, IPatientRepository patientRepo)
        {
            _doctorRepo = doctorRepo;
            _patientRepo = patientRepo;
        }

        // Handles doctor registration logic
        public async Task<string> RegisterDoctor(DoctorRegisterDto dto)
        {
            // Check if doctor username already exists
            if (await _doctorRepo.DoctorExists(dto.Username))
                return "Doctor username already exists.";

            // Map DTO to Doctor entity
            var doctor = new Doctor
            {
                FullName = dto.FullName,
                Specialty = dto.Specialty,
                Username = dto.Username,
                Password = dto.Password 
            };

            // Save doctor to database
            await _doctorRepo.Register(doctor);
            return "Doctor registered successfully.";
        }

        // Handles patient registration logic
        public async Task<string> RegisterPatient(PatientRegisterDto dto)
        {
            // Check if patient username already exists
            if (await _patientRepo.PatientExists(dto.Username))
                return "Patient username already exists.";

            // Map DTO to Patient entity
            var patient = new Patient
            {
                FullName = dto.FullName,
                Age = dto.Age,
                Username = dto.Username,
                Password = dto.Password // In real apps, hash the password
            };

            // Save patient to database
            await _patientRepo.Register(patient);
            return "Patient registered successfully.";
        }

        // Handles doctor login logic
        public async Task<string> LoginDoctor(LoginDto dto)
        {
            // Try to find doctor by username and password
            var doctor = await _doctorRepo.Login(dto.Username, dto.Password);

            // Return appropriate message
            return doctor != null ? "Doctor login successful." : "Invalid doctor credentials.";
        }

        // Handles patient login logic
        public async Task<string> LoginPatient(LoginDto dto)
        {
            // Try to find patient by username and password
            var patient = await _patientRepo.Login(dto.Username, dto.Password);

            // Return appropriate message
            return patient != null ? "Patient login successful." : "Invalid patient credentials.";
        }
    }
}
