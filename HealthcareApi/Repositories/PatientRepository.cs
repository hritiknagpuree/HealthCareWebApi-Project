using HealthcareApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApi.Repositories
{
    // Handles Patient data operations
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _context;

        // Inject DB context
        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }

        // Register new patient
        public async Task<Patient> Register(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        // Patient login
        public async Task<Patient> Login(string username, string password)
        {
            return await _context.Patients
                .FirstOrDefaultAsync(p => p.Username == username && p.Password == password);
        }

        // Check if username exists
        public async Task<bool> PatientExists(string username)
        {
            return await _context.Patients.AnyAsync(p => p.Username == username);
        }

        // Get all patients
        public async Task<IEnumerable<Patient>> GetAllPatients()
        {
            return await _context.Patients.ToListAsync();
        }

        // Interface implementation
        Task IPatientRepository.Register(Patient patient)
        {
            return Register(patient);
        }

        // Not implemented
        public Task AddPatient(Patient patient)
        {
            throw new NotImplementedException();
        }

        // Get patient by username
        public async Task<Patient> GetByUsername(string username)
        {
            return await _context.Patients.FirstOrDefaultAsync(p => p.Username == username);
        }

        // Not implemented
        Task<Patient> IPatientRepository.AddPatient(Patient patient)
        {
            throw new NotImplementedException();
        }
    }
}
