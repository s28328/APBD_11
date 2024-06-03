using APBD_10.Models;
using APBD_10.RequestModels;

namespace APBD_10.IRepositories;

public interface IPatientRepository
{
    public Task<Patient?> GetPatient(Patient patient);
    public Task<Patient?> GetPatient(int idPatient);
    public Task<int> AddPatient(PatientDto patient);
}