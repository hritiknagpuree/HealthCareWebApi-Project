using Microsoft.EntityFrameworkCore;

public class DoctorRepository : IDoctorRepository
{
    private readonly AppDbContext _context;

    public DoctorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> DoctorExists(string username)
    {
        return await _context.Doctors.AnyAsync(d => d.Username == username);
    }

    public async Task Register(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();
    }

    public async Task<Doctor?> Login(string username, string password)
    {
        return await _context.Doctors
            .FirstOrDefaultAsync(d => d.Username == username && d.Password == password);
    }

    public async Task<IEnumerable<Doctor>> GetAllDoctors()
    {
        return await _context.Doctors.ToListAsync();
    }
}
