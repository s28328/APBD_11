using System.Text;
using APBD_10.Context;
using APBD_10.IRepositories;
using APBD_10.Middlewares;
using APBD_10.Repositories;
using APBD_10.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IMedicamentRepository, MedicamentRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
builder.Services.AddScoped<IPresMedRepository, PresMedRepository>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddDbContext<HospitalDbContext>(
    options => options.UseNpgsql("Host=localhost; Database=apbd; Username=postgres; Password=admin; Pooling = true;"));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,   //by who
        ValidateAudience = true, //for whom
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(2),
        ValidIssuer = "https://localhost:5145", //should come from configuration
        ValidAudience = "https://localhost:5145", //should come from configuration
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SecretKey"]))
    };

    opt.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Token-expired", "true");
            }
            return Task.CompletedTask;
        }
    };
}).AddJwtBearer("IgnoreTokenExpirationScheme",opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,   //by who
        ValidateAudience = true, //for whom
        ValidateLifetime = false,
        ClockSkew = TimeSpan.FromMinutes(2),
        ValidIssuer = "https://localhost:5145", //should come from configuration
        ValidAudience = "https://localhost:5145", //should come from configuration
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SecretKey"]))
    };
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.MapControllers();

app.Run();
