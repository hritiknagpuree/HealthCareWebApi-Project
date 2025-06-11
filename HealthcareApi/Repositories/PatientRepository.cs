using HealthcareApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApi.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _context;

        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Patient> Register(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<Patient> Login(string username, string password)
        {
            return await _context.Patients
                .FirstOrDefaultAsync(p => p.Username == username && p.Password == password);
        }

        public async Task<bool> PatientExists(string username)
        {
            return await _context.Patients.AnyAsync(p => p.Username == username);
        }
        public async Task<IEnumerable<Patient>> GetAllPatients()
        {
            return await _context.Patients.ToListAsync();
        }

        Task IPatientRepository.Register(Patient patient)
        {
            return Register(patient);
        }

        public Task AddPatient(Patient patient)
        {
            throw new NotImplementedException();
        }
        public async Task<Patient> GetByUsername(string username)
{
    return await _context.Patients.FirstOrDefaultAsync(p => p.Username == username);
}


        Task<Patient> IPatientRepository.AddPatient(Patient patient)
        {
            throw new NotImplementedException();
        }
    }
}
