using APBD_10.Models;

namespace APBD_10.IRepositories;

public interface IDoctorRepository
{
    public Task<Doctor?> GetDoctor(int idDoctor);
}