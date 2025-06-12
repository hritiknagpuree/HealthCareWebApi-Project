using Microsoft.EntityFrameworkCore;
using HealthcareApi.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

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

    // Get doctor by ID
    public async Task<Doctor?> GetDoctorById(int id)
    {
        return await _context.Doctors.FindAsync(id);
    }

    // Update doctor
    public async Task<bool> UpdateDoctor(Doctor doctor)
    {
        _context.Doctors.Update(doctor);
        return await _context.SaveChangesAsync() > 0;
    }

    // Delete doctor
    public async Task<bool> DeleteDoctor(Doctor doctor)
    {
        _context.Doctors.Remove(doctor);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Doctor?> GetDoctorById(Guid id)
    {
        return await _context.Doctors.FindAsync(id);
    }
}
