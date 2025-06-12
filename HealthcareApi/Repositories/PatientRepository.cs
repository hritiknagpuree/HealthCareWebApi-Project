using HealthcareApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task Register(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        // Patient login
        public async Task<Patient?> Login(string username, string password)
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

        // Add a new patient and return it
        public async Task<Patient> AddPatient(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        // Get patient by username
        public async Task<Patient?> GetByUsername(string username)
        {
            return await _context.Patients.FirstOrDefaultAsync(p => p.Username == username);
        }

        // Get patient by ID
        public async Task<Patient?> GetPatientById(Guid id)
        {
            return await _context.Patients.FindAsync(id);
        }

        // Update existing patient
        public async Task<bool> UpdatePatient(Patient patient)
        {
            _context.Patients.Update(patient);
            return await _context.SaveChangesAsync() > 0;
        }

        // Delete patient
        public async Task<bool> DeletePatient(Patient patient)
        {
            _context.Patients.Remove(patient);
            return await _context.SaveChangesAsync() > 0;
        }

        public Task<Patient?> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
