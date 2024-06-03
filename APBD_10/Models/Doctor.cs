using System.ComponentModel.DataAnnotations;

namespace APBD_10.Models;

public class Doctor
{
    public Doctor()
    {
        Prescriptions =new List<Prescription>();
    }

    [Key]
    public int IdDoctor { get; set; }
    [MaxLength(100)]
    [Required]
    public string FirstName { get; set; }
    [MaxLength(100)]
    [Required]
    public string LastName { get; set; }
    [MaxLength(100)]
    [Required]
    public string Email { get; set; }

    public ICollection<Prescription> Prescriptions { get; set; } 
    
}