using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthcareApi.Interfaces
{
    // Interface for patient data operations
    public interface IPatientRepository
    {
        // Check if patient username exists
        Task<bool> PatientExists(string username);

        // Register a new patient
        Task Register(Patient patient);

        // Login patient
        Task<Patient?> Login(string username, string password);

        // Get all patients
        Task<IEnumerable<Patient>> GetAllPatients();

        // Add a patient (used internally for testing or admin tools)
        Task<Patient> AddPatient(Patient patient);

        // Get patient by username
        Task<Patient> GetByUsername(string username);

        // Get patient by ID
        Task<Patient?> GetById(Guid id);

        // Update patient
        Task<bool> UpdatePatient(Patient patient);

        // Delete patient
        Task<bool> DeletePatient(Patient patient);
        Task<Patient?> GetPatientById(Guid id);
      

    }
}
