using APBD_10.Models;

namespace APBD_10.ResponseModels;

public class PatientInfo
{
    public int  IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public List<PrescriptionInfo> Prescriptions { get; set; }
}