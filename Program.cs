using EntityFramworkProject.Automappers;
using EntityFramworkProject.DTOs;
using EntityFramworkProject.Models;
using EntityFramworkProject.Repository;
using EntityFramworkProject.Services;
using EntityFramworkProject.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Dependency inyection
builder.Services.AddKeyedScoped<ICommonService<EmployeeDTO, EmployeeInsertDTO, EmployeeUpdateDTO>, EmployeeService>("employeeService");
builder.Services.AddKeyedScoped<ICommonService<ChildrenDTO, ChildrenInsertDTO, ChildrenUpdateDTO>, ChildrenService>("childrenService");

// Repository
builder.Services.AddScoped<IRepository<Employee>, EmployeeRepository>();
builder.Services.AddScoped<IRepository<Children>, ChildrenRepository>();

// Entity Framework
builder.Services.AddDbContext<ProgramContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProgramConnection"));
});

// Validators
builder.Services.AddScoped<IValidator<EmployeeInsertDTO>, EmployeeInsertValidation>();
builder.Services.AddScoped<IValidator<EmployeeUpdateDTO>, EmployeeUpdateValidator>();

builder.Services.AddScoped<IValidator<ChildrenInsertDTO>, ChildrenInsertValidator>();
builder.Services.AddScoped<IValidator<ChildrenUpdateDTO>, ChildrenUpdateValidator>();

// Mappers
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

