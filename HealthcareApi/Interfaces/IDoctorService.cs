using HealthcareApi.ViewModels;

namespace HealthcareApi.Interfaces
{
    public interface IDoctorService
    {
        Task<string> RegisterDoctor(DoctorRegisterDto dto);
        Task<string> LoginDoctor(LoginDto dto);
        Task<IEnumerable<DoctorViewModel>> GetAllDoctors();
    }
}
