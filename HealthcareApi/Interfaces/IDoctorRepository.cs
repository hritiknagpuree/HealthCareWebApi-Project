public interface IDoctorRepository
{
    Task<bool> DoctorExists(string username);
    Task Register(Doctor doctor);
    Task<Doctor?> Login(string username, string password);
    Task<IEnumerable<Doctor>> GetAllDoctors(); //  Must return Doctor type
}
