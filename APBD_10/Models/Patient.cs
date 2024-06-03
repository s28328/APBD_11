using System.ComponentModel.DataAnnotations;

namespace APBD_10.Models;

public class Patient
{
    public Patient()
    {
        Prescriptions = new List<Prescription>();
    }
    
    [Key]
    public int IdPatient { get; set; }
    [MaxLength(100)]
    [Required]
    public string FirstName { get; set; }
    [MaxLength(100)]
    [Required]
    public string LastName { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    public ICollection<Prescription> Prescriptions { get; set; }
}