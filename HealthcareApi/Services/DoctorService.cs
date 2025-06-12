using HealthcareApi.Interfaces;
using HealthcareApi.ViewModels;

namespace HealthcareApi.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repo;

        public DoctorService(IDoctorRepository repo)
        {
            _repo = repo;
        }

        // Register new doctor
        public async Task<string> RegisterDoctor(DoctorRegisterDto dto)
        {
            if (await _repo.DoctorExists(dto.Username))
                return "Doctor username already exists.";

            var doctor = new Doctor
            {
                Id = Guid.NewGuid(), // Generate a new Guid
                FullName = dto.FullName,
                Specialty = dto.Specialty,
                Username = dto.Username,
                Password = dto.Password // Note: hash in production
            };

            await _repo.Register(doctor);
            return "Doctor registered successfully.";
        }

        // Login doctor
        public async Task<string> LoginDoctor(LoginDto dto)
        {
            var doctor = await _repo.Login(dto.Username, dto.Password);

            if (doctor != null)
            {
                DoctorSession.IsDoctorLoggedIn = true;
                return "Login successful";
            }

            return "Invalid credentials";
        }

        // Get all doctors
        public async Task<IEnumerable<DoctorViewModel>> GetAllDoctors()
        {
            var doctors = await _repo.GetAllDoctors();

            return doctors.Select(d => new DoctorViewModel
            {
                Id = d.Id,
                FullName = d.FullName,
                Specialty = d.Specialty
            });
        }

        // Get doctor by ID
        public async Task<DoctorViewModel?> GetDoctorById(Guid id)
        {
            var doctor = await _repo.GetDoctorById(id);
            if (doctor == null) return null;

            return new DoctorViewModel
            {
                Id = doctor.Id,
                FullName = doctor.FullName,
                Specialty = doctor.Specialty
            };
        }

        // Update doctor
        public async Task<bool> UpdateDoctor(Guid id, DoctorUpdateDto dto)
        {
            var doctor = await _repo.GetDoctorById(id);
            if (doctor == null) return false;

            doctor.FullName = dto.FullName;
            doctor.Specialty = dto.Specialty;
            doctor.Password = dto.Password; // Optional

            return await _repo.UpdateDoctor(doctor);
        }

        // Delete doctor
        public async Task<bool> DeleteDoctor(Guid id)
        {
            var doctor = await _repo.GetDoctorById(id);
            if (doctor == null) return false;

            return await _repo.DeleteDoctor(doctor);
        }
    }
}
