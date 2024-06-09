using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using APBD_10.Helpers;
using APBD_10.IRepositories;
using APBD_10.Models;
using APBD_10.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using LoginRequest = APBD_10.RequestModels.LoginRequest;
using RegisterRequest = APBD_10.RequestModels.RegisterRequest;

namespace APBD_10.Services;

public class AppUserService:IAppUserService
{
    private readonly IAppUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AppUserService(IAppUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<int> RegisterUser(RegisterRequest model)
    {
        var hashedPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt(model.Password);

        //alaMaKota
        //hash(alaMaKota+salt1+pepper)=>sdsd3dfgsd3fdfdfdfdsfsfsdfsdfsdf
        //hash(alaMaKota+salt2+pepper)=>df4htghdfgdfg32fedfdfsfq23fedfdd


        var user = new AppUser()
        {
            Email = model.Email,
            Login = model.Login,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            RefreshToken = SecurityHelpers.GenerateRefreshToken(),
            RefreshTokenExp = DateTime.Now.AddDays(1)
        };
        await _userRepository.AddUser(user);
        return 1;
    }

    public async Task<Object?> LoginUser(LoginRequest loginRequest)
    {
        AppUser user = await _userRepository.GetUserByLogin(loginRequest.Login);

        string passwordHashFromDb = user.Password;
        string curHashedPassword = SecurityHelpers.GetHashedPasswordWithSalt(loginRequest.Password, user.Salt);

        if (passwordHashFromDb != curHashedPassword)
        {
            return new UnauthorizedResult();
        }
        Claim[] userclaim = new[]
        {
            new Claim(ClaimTypes.Name, "dnovo"),
            new Claim(ClaimTypes.Role, "user"),
            new Claim(ClaimTypes.Role, "admin")
            //Add additional data here
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: "https://localhost:5145",
            audience: "https://localhost:5145",
            claims: userclaim,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );

        user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        await _userRepository.SaveChanges();
        return new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(token),
            refreshToken = user.RefreshToken
        };
    }

    public async Task<Object?> RefreshUserToken(RefreshTokenRequest tokenRequest)
    {
        var user = await _userRepository.GetUserByRefreshToken(tokenRequest.RefreshToken);
        if (user == null)
        {
            throw new SecurityTokenException("Invalid refresh token");
        }

        if (user.RefreshTokenExp < DateTime.Now)
        {
            throw new SecurityTokenException("Refresh token expired");
        }
        
        Claim[] userclaim = new[]
        {
            new Claim(ClaimTypes.Name, "dnovo"),
            new Claim(ClaimTypes.Role, "user"),
            new Claim(ClaimTypes.Role, "admin")
            //Add additional data here
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtToken = new JwtSecurityToken(
            issuer: "https://localhost:5145",
            audience: "https://localhost:5145",
            claims: userclaim,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );

        user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        await _userRepository.SaveChanges();

        return new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
            refreshToken = user.RefreshToken
        };
    }
}