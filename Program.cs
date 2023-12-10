using Microsoft.EntityFrameworkCore;
using Psinder.Data;
using Psinder.Helpers;
using Psinder.Middleware;
using Psinder.Repositories;
using Psinder.Repositories.Interfaces;
using Psinder.Services;
using Psinder.Services.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PsinderDb>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("Psinder")));
builder.Services.AddScoped<ShelterSeeder>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IShelterRepository, ShelterRepository>();
builder.Services.AddScoped<IShelterService, ShelterService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<ShelterSeeder>();

seeder.Seed();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
