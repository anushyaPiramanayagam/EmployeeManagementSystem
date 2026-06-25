using EmployeeManagement.Infrastructure.Configurations;
using EmployeeManagement.Application.Mappings;
using EmployeeManagement.API.Extensions;
using Serilog;
using FluentValidation;
using EmployeeManagement.Application.Validators;
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(
        "Logs/log-.txt",
        rollingInterval:
        RollingInterval.Day)
    .CreateLogger();
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(cfg =>
{
}, typeof(EmployeeProfile).Assembly);
builder.Services.AddValidatorsFromAssemblyContaining<CreateEmployeeDtoValidator>(); 
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseGlobalExceptionHandling();
app.MapControllers();

app.Run();