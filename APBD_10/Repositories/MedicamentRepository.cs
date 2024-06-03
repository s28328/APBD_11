using APBD_10.Context;
using APBD_10.IRepositories;
using APBD_10.Models;
using APBD_10.RequestModels;
using Microsoft.EntityFrameworkCore;

namespace APBD_10.Repositories;

public class MedicamentRepository:IMedicamentRepository
{
    private readonly HospitalDbContext _dbContext;

    public MedicamentRepository(HospitalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> AllMedicamentsExist(List<RequestMedicament> medicaments)
    {
        var medicamentIds = medicaments.Select(m => m.idMedicament).ToList();
        
        var existingMedicamentIds = await _dbContext.Medicaments
            .Where(m => medicamentIds.Contains(m.IdMedicament))
            .Select(m => m.IdMedicament)
            .ToListAsync();
        
        return medicamentIds.All(id => existingMedicamentIds.Contains(id));
    }

}