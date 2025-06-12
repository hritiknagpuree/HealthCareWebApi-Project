using HealthcareApi.ViewModels;

namespace HealthcareApi.Interfaces
{
    public interface IDoctorService
    {
        // Register new doctor
        Task<string> RegisterDoctor(DoctorRegisterDto dto);

        // Doctor login
        Task<string> LoginDoctor(LoginDto dto);

        // Get all doctors as view models
        Task<IEnumerable<DoctorViewModel>> GetAllDoctors();

        // Get doctor by ID (Guid)
        Task<DoctorViewModel?> GetDoctorById(Guid id);

        // Update doctor
        Task<bool> UpdateDoctor(Guid id, DoctorUpdateDto dto);

        // Delete doctor
        Task<bool> DeleteDoctor(Guid id);
    }
}
