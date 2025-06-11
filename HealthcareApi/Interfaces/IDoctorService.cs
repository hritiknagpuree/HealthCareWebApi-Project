using HealthcareApi.ViewModels;

namespace HealthcareApi.Interfaces
{
    // Doctor service interface
    public interface IDoctorService
    {
        // Register new doctor
        Task<string> RegisterDoctor(DoctorRegisterDto dto);

        // Doctor login
        Task<string> LoginDoctor(LoginDto dto);

        // Get all doctors as view models
        Task<IEnumerable<DoctorViewModel>> GetAllDoctors();
    }
}
