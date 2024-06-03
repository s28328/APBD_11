using System.Text.Json.Serialization.Metadata;
using APBD_10.Exceptions;
using APBD_10.IRepositories;
using APBD_10.Models;
using APBD_10.RequestModels;

namespace APBD_10.Services;

public class PrescriptionService:IPrescriptionService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMedicamentRepository _medicamentRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly IPresMedRepository _presMedRepository;

    public PrescriptionService(IPatientRepository patientRepository, IMedicamentRepository medicamentRepository, IDoctorRepository doctorRepository, IPrescriptionRepository prescriptionRepository, IPresMedRepository presMedRepository)
    {
        _patientRepository = patientRepository;
        _medicamentRepository = medicamentRepository;
        _doctorRepository = doctorRepository;
        _prescriptionRepository = prescriptionRepository;
        _presMedRepository = presMedRepository;
    }

    public async Task<int> CreatePrescription(CreatePrescription createPrescription)
    {
        var patient = await _patientRepository.GetPatient(createPrescription.Patient.IdPatient);
        if (patient == null)
        {
            var affectedCount = _patientRepository.AddPatient(createPrescription.Patient);
        }
        await AllMedicamentsExist(createPrescription.Medicaments);
        var doctor = await DoctorExist(createPrescription.IdDoctor);
        MedicamentsOutOfLimit(10, createPrescription.Medicaments);
        ValidDates(createPrescription.Date, createPrescription.DueDate);
        var idPrescription = await _prescriptionRepository.AddPrescription(new Prescription()
        {
            Date = createPrescription.Date,
            DueDate = createPrescription.DueDate,
            IdDoctor = createPrescription.IdDoctor,
            IdPatient = createPrescription.Patient.IdPatient
        });
        foreach (var medicament in createPrescription.Medicaments)
        {
            var presMed = new PrescriptionMedicament()
            {
                IdMedicament = medicament.idMedicament,
                IdPrescription = idPrescription,
                Dose = medicament.Dose,
                Details = medicament.Description
            };
            await _presMedRepository.AddPresMed(presMed);
        }
        return 1;
    }

    private async Task<bool> AllMedicamentsExist(List<RequestMedicament> medicaments)
    {
        var exist = await _medicamentRepository.AllMedicamentsExist(medicaments);
        if (!exist)
        {
            throw new DomainException("Provided medicament does not exist.");
        }

        return true;
    }

    private async Task<Doctor> DoctorExist(int idDoctor)
    {
        var doctor = await _doctorRepository.GetDoctor(idDoctor);
        if (doctor == null)
        {
            throw new DomainException("Provided doctor does not exist.");
        }

        return doctor;
    }

    private bool MedicamentsOutOfLimit(int limit, List<RequestMedicament> medicaments)
    {
        var outOfLimit = medicaments.Count > limit;
        if (outOfLimit)
        {
            throw new DomainException("Count of medicament's can`t be higher than "+limit);

        }

        return false;
    }

    private bool ValidDates(DateTime Date, DateTime DueDate)
    {
        if (Date > DueDate)
        {
            throw new DomainException("Date and DueDate is not valid.");
        }

        return true;
    }
}