using APBD_10.Exceptions;
using APBD_10.IRepositories;
using APBD_10.ResponseModels;

namespace APBD_10.Services;

public class PatientService:IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly IPresMedRepository _presMedRepository;
    private readonly IMedicamentRepository _medicamentRepository;
    private readonly IDoctorRepository _doctorRepository;

    public PatientService(IPatientRepository patientRepository, IPrescriptionRepository prescriptionRepository, IPresMedRepository presMedRepository, IMedicamentRepository medicamentRepository, IDoctorRepository doctorRepository)
    {
        _patientRepository = patientRepository;
        _prescriptionRepository = prescriptionRepository;
        _presMedRepository = presMedRepository;
        _medicamentRepository = medicamentRepository;
        _doctorRepository = doctorRepository;
    }

    public async Task<PatientInfo> GetPatientInfo(int id)
    {
        var patient = await _patientRepository.GetPatient(id);
        if (patient == null)
        {
            throw new DomainException("No patient with provided Id.");
        }

        var prescriptionInfoList = patient.Prescriptions.Select(p => new PrescriptionInfo()
        {
            Date = p.Date,
            DueDate = p.DueDate,
            IdPrescription = p.IdPrescription,
            Doctor = new DoctorDto()
            {
                IdDoctor = p.Doctor.IdDoctor,
                FirstName = p.Doctor.FirstName,
                LastName = p.Doctor.LastName,
                Email = p.Doctor.Email
            }
        }).ToList();
        
        foreach (var prescriptionInfo in prescriptionInfoList)
        {
            var prescription = patient.Prescriptions.
                First(p => p.IdPrescription == prescriptionInfo.IdPrescription);
            var presMedsInfoList =prescription.PrescriptionMedicaments.
                Select(pm =>
                new PresMedInfo()
                {
                    IdMedicament = pm.IdMedicament,
                    Name = pm.Medicament.Name,
                    Description = pm.Medicament.Description,
                    Dose = pm.Dose
                }).ToList();
            prescriptionInfo.Medicaments = presMedsInfoList;
        }
        
        var patientInfo = new PatientInfo()
        {
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            IdPatient = patient.IdPatient,
            Prescriptions = prescriptionInfoList
        };
        return patientInfo;
    }
    
}