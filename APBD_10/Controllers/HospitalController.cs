using APBD_10.Exceptions;
using APBD_10.RequestModels;
using APBD_10.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_10.Controllers;
[Route("api/[controller]")]
[ApiController]
public class HospitalController:ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;
    private readonly IPatientService _patientService;

    public HospitalController(IPrescriptionService prescriptionService, IPatientService patientService)
    {
        _prescriptionService = prescriptionService;
        _patientService = patientService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePrescription(CreatePrescription createPrescription)
    {
        try
        {
            return Ok(await _prescriptionService.CreatePrescription(createPrescription));
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPatientInfo(int id)
    {
        try
        {
            var result = await _patientService.GetPatientInfo(id);
            return Ok(result);
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
}