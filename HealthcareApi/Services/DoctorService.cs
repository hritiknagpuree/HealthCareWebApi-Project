using HealthcareApi.Interfaces;
using HealthcareApi.ViewModels;

namespace HealthcareApi.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepo;

        public DoctorService(IDoctorRepository repo)
        {
            _doctorRepo = repo;
        }

        // Register new doctor
        public async Task<string> RegisterDoctor(DoctorRegisterDto dto)
        {
            if (await _doctorRepo.DoctorExists(dto.Username))
                return "Doctor username already exists.";

            var doctor = new Doctor
            {
                Id = Guid.NewGuid(), // Generate a new Guid
                FullName = dto.FullName,
                Specialty = dto.Specialty,
                Username = dto.Username,
                Password = dto.Password 
            };

            await _doctorRepo.Register(doctor);
            return "Doctor registered successfully.";
        }

        // Login doctor
        public async Task<string> LoginDoctor(LoginDto dto)
        {
            var doctor = await _doctorRepo.Login(dto.Username, dto.Password);

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
            var doctors = await _doctorRepo.GetAllDoctors();

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
            var doctor = await _doctorRepo.GetDoctorById(id);
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
            var doctor = await _doctorRepo.GetDoctorById(id);
            if (doctor == null) return false;

            doctor.FullName = dto.FullName;
            doctor.Specialty = dto.Specialty;
            doctor.Password = dto.Password; 

            return await _doctorRepo.UpdateDoctor(doctor);
        }

        // Delete doctor
        public async Task<bool> DeleteDoctor(Guid id)
        {
            var doctor = await _doctorRepo.GetDoctorById(id);
            if (doctor == null) return false;

            return await _doctorRepo.DeleteDoctor(doctor);
        }
    }
}
