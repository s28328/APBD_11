using APBD_10.Models;
using APBD_10.RequestModels;

namespace APBD_10.IRepositories;

public interface IMedicamentRepository
{
    public Task<bool> AllMedicamentsExist(List<RequestMedicament> medicaments);

}