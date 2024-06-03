using APBD_10.Models;

namespace APBD_10.IRepositories;

public interface IPresMedRepository
{
    public Task<int> AddPresMed(PrescriptionMedicament presMed);

}