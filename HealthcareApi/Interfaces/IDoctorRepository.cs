public interface IDoctorRepository
{
    // Check if doctor username exists
    Task<bool> DoctorExists(string username);

    // Register new doctor
    Task Register(Doctor doctor);

    // Login doctor by username and password
    Task<Doctor?> Login(string username, string password);

    // Get all doctors
    Task<IEnumerable<Doctor>> GetAllDoctors(); // Must return Doctor type
}
