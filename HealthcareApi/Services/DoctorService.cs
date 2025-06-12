using HealthcareApi.Interfaces;
using HealthcareApi.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareApi.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repo;

        public DoctorService(IDoctorRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> RegisterDoctor(DoctorRegisterDto dto)
        {
            if (await _repo.DoctorExists(dto.Username))
                return "Doctor username already exists.";

            var doctor = new Doctor
            {
                FullName = dto.FullName,
                Specialty = dto.Specialty,
                Username = dto.Username,
                Password = dto.Password 
            };

            await _repo.Register(doctor);
            return "Doctor registered successfully.";
        }

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
    }
}
