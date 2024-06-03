using APBD_10.Context;
using APBD_10.IRepositories;
using APBD_10.Models;

namespace APBD_10.Repositories;

public class PresMedRepository:IPresMedRepository
{
    private readonly HospitalDbContext _dbContext;

    public PresMedRepository(HospitalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> AddPresMed(PrescriptionMedicament presMed)
    {
        await _dbContext.PrescriptionMedicaments.AddAsync(presMed);
        await _dbContext.SaveChangesAsync();
        return 1;
    }
}