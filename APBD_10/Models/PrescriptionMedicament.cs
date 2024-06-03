using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_10.Models;
[Table("Prescription_Medicament")]
public class PrescriptionMedicament
{
    [ForeignKey("Medicament")]
    public int IdMedicament { get; set; }
    [ForeignKey("Prescription")]
    public int IdPrescription { get; set; }
    [Required]
    public int Dose { get; set; }
    [Required]
    [MaxLength(100)]
    public string Details { get; set; }
    
 //   [ForeignKey(nameof(IdMedicament))] 
    public Medicament Medicament { get; set; }
    
   // [ForeignKey(nameof(IdPrescription))]
    public Prescription Prescription { get; set; }
}