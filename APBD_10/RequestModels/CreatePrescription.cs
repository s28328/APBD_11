using APBD_10.Models;

namespace APBD_10.RequestModels;

public class CreatePrescription
{
    public PatientDto Patient { get; set; }
    public int IdDoctor { get; set; }
    public List<RequestMedicament> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    
}