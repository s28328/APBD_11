using APBD_10.Context;
using APBD_10.IRepositories;
using APBD_10.Repositories;
using APBD_10.Services;
using Microsoft.EntityFrameworkCore;
WebApplicationOptions options = new WebApplicationOptions {
    ContentRootPath = "D:\\APBD\\APBD_10"
};
var builder = WebApplication.CreateBuilder(options);

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
builder.Services.AddDbContext<HospitalDbContext>(
    options => options.UseNpgsql("Host=localhost; Database=apbd; Username=postgres; Password=admin; Pooling = true;"));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
