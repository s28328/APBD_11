using APBD_10.Context;
using APBD_10.IRepositories;
using APBD_10.Models;

namespace APBD_10.Repositories;

public class PrescriptionRepository:IPrescriptionRepository
{
    private readonly HospitalDbContext _dbContext;

    public PrescriptionRepository(HospitalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> AddPrescription(Prescription prescription)
    {
        await _dbContext.Prescriptions.AddAsync(prescription);
        await _dbContext.SaveChangesAsync();
        return prescription.IdPrescription;
    }
}