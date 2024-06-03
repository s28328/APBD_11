using APBD_10.Context;
using APBD_10.IRepositories;
using APBD_10.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_10.Repositories;

public class DoctorRepository:IDoctorRepository
{
    private readonly HospitalDbContext _dbContext;

    public DoctorRepository(HospitalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Doctor?> GetDoctor(int idDoctor)
    {
        return await _dbContext.Doctors.FirstOrDefaultAsync(doctor => doctor.IdDoctor == idDoctor);
    }
}