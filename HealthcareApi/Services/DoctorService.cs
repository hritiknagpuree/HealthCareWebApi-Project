using HealthcareApi.Interfaces;
using HealthcareApi.ViewModels;

namespace HealthcareApi.Services
{
    // Service class to handle doctor-related logic
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repo;

        // Inject the doctor repository
        public DoctorService(IDoctorRepository repo)
        {
            _repo = repo;
        }

        // Register a new doctor
        public async Task<string> RegisterDoctor(DoctorRegisterDto dto)
        {
            // Check if the username is already taken
            if (await _repo.DoctorExists(dto.Username))
                return "Doctor username already exists.";

            // Create a doctor object from the DTO
            var doctor = new Doctor
            {
                FullName = dto.FullName,
                Specialty = dto.Specialty,
                Username = dto.Username,
                Password = dto.Password
            };

            // Save to database
            await _repo.Register(doctor);
            return "Doctor registered successfully.";
        }

        // Login doctor with credentials
        public async Task<string> LoginDoctor(LoginDto dto)
        {
            var doctor = await _repo.Login(dto.Username, dto.Password);
            return doctor != null ? "Doctor login successful." : "Invalid credentials.";
        }

        // Get list of all doctors
        public async Task<IEnumerable<DoctorViewModel>> GetAllDoctors()
        {
            var doctors = await _repo.GetAllDoctors();

            // Map to view models
            return doctors.Select(d => new DoctorViewModel
            {
                Id = d.Id,
                FullName = d.FullName,
                Specialty = d.Specialty
            });
        }
    }
}
