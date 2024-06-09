using APBD_10.Exceptions;
using APBD_10.Helpers;
using APBD_10.Models;
using APBD_10.RequestModels;
using APBD_10.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = APBD_10.RequestModels.LoginRequest;
using RegisterRequest = APBD_10.RequestModels.RegisterRequest;

namespace APBD_10.Controllers;
[Route("api/[controller]")]
[ApiController]
public class HospitalController:ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;
    private readonly IPatientService _patientService;
    private readonly IAppUserService _userService;

    public HospitalController(IPrescriptionService prescriptionService, IPatientService patientService, IAppUserService userService)
    {
        _prescriptionService = prescriptionService;
        _patientService = patientService;
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePrescription(CreatePrescription createPrescription)
    { 
        return Ok(await _prescriptionService.CreatePrescription(createPrescription));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPatientInfo(int id)
    {
        var result = await _patientService.GetPatientInfo(id);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterStudent(RegisterRequest model)
    {
        await _userService.RegisterUser(model);
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var result = await _userService.LoginUser(loginRequest);

        return Ok(result);
    }

    [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest refreshToken)
    {
       var result = await _userService.RefreshUserToken(refreshToken);
       return Ok(result);
    }
}