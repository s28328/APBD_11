using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_10.Models;

public class Prescription
{
    public Prescription()
    {
        PrescriptionMedicaments =new List<PrescriptionMedicament>();
    }

    [Key]
    public int IdPrescription { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    [Required]
    public int IdPatient { get; set; }
    [Required]
    public int IdDoctor { get; set; }
    
    [Required]
    [ForeignKey(nameof(IdDoctor))]
    public Doctor Doctor { get; set; }
    
    [Required]
    [ForeignKey(nameof(IdPatient))]
    public Patient Patient { get; set; }
    
    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    
    
}