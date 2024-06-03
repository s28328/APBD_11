using APBD_10.Models;

namespace APBD_10.IRepositories;

public interface IPrescriptionRepository
{
    public Task<int> AddPrescription(Prescription prescription);
}