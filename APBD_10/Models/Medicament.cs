using System.ComponentModel.DataAnnotations;

namespace APBD_10.Models;

public class Medicament
{
    public Medicament()
    {
        PrescriptionMedicaments =new List<PrescriptionMedicament>();
    }

    [Key]
    public int IdMedicament { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    [MaxLength(100)]
    public string Description { get; set; }
    [Required]
    [MaxLength(100)]
    public string Type { get; set; }

    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    
}