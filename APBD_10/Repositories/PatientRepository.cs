using APBD_10.Context;
using APBD_10.IRepositories;
using APBD_10.Models;
using APBD_10.RequestModels;
using Microsoft.EntityFrameworkCore;

namespace APBD_10.Repositories;

public class PatientRepository:IPatientRepository
{
    private readonly HospitalDbContext _dbContext;

    public PatientRepository(HospitalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Patient?> GetPatient(Patient patient)
    {
        bool exists = await _dbContext.Patients.ContainsAsync(patient);
        return exists ? patient : null;
    }

    public async Task<Patient?> GetPatient(int idPatient)
    {
        return await _dbContext.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(p => p.Doctor)
            .Include(p => p.Prescriptions)
            .ThenInclude(p => p.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .FirstOrDefaultAsync(p => p.IdPatient == idPatient);
        
    }

    public async Task<int> AddPatient(PatientDto patient)
    {
        await _dbContext.Patients.AddAsync(new Patient()
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            BirthDate = patient.BirthDate
            
        });
        await _dbContext.SaveChangesAsync();
        return 1;
    }
}