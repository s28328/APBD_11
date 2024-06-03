using APBD_10.ResponseModels;

namespace APBD_10.Services;

public interface IPatientService
{
    public Task<PatientInfo> GetPatientInfo(int id);

}