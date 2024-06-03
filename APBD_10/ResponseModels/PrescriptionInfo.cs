namespace APBD_10.ResponseModels;

public class PrescriptionInfo
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<PresMedInfo> Medicaments { get; set; }
    public DoctorDto Doctor{ get; set; }
}