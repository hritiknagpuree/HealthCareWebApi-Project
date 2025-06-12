public interface IDoctorRepository
{
    // Check if doctor username exists
    Task<bool> DoctorExists(string username);

    // Register new doctor
    Task Register(Doctor doctor);

    // Login doctor by username and password
    Task<Doctor?> Login(string username, string password);

    // Get all doctors
    Task<IEnumerable<Doctor>> GetAllDoctors();

    // Get doctor by Guid ID
    Task<Doctor?> GetDoctorById(Guid id);

    // Update doctor
    Task<bool> UpdateDoctor(Doctor doctor);

    // Delete doctor
    Task<bool> DeleteDoctor(Doctor doctor);
}
