using Microsoft.EntityFrameworkCore;

// Handles Doctor data operations
public class DoctorRepository : IDoctorRepository
{
    private readonly AppDbContext _context;

    // Inject DB context
    public DoctorRepository(AppDbContext context)
    {
        _context = context;
    }

    // Check if doctor username exists
    public async Task<bool> DoctorExists(string username)
    {
        return await _context.Doctors.AnyAsync(d => d.Username == username);
    }

    // Add new doctor
    public async Task Register(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();
    }

    // Doctor login
    public async Task<Doctor?> Login(string username, string password)
    {
        return await _context.Doctors
            .FirstOrDefaultAsync(d => d.Username == username && d.Password == password);
    }

    // Get all doctors
    public async Task<IEnumerable<Doctor>> GetAllDoctors()
    {
        return await _context.Doctors.ToListAsync();
    }
}
