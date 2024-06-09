using APBD_10.RequestModels;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = APBD_10.RequestModels.LoginRequest;
using RegisterRequest = APBD_10.RequestModels.RegisterRequest;

namespace APBD_10.Services;

public interface IAppUserService
{
    public Task<int> RegisterUser(RegisterRequest model);
    public Task<object?> LoginUser(LoginRequest loginRequest);
    public Task<object?> RefreshUserToken(RefreshTokenRequest tokenRequest);
}