using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Psinder.Data;
using Psinder.Helpers;
using Psinder.Middleware;
using Psinder.Repositories;
using Psinder.Repositories.Interfaces;
using Psinder.Services;
using Psinder.Services.Interfaces;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PsinderDb>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("Psinder")));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IShelterRepository, ShelterRepository>();
builder.Services.AddScoped<IShelterService, ShelterService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
builder.Services.AddScoped<IAnimalService, AnimalService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAdminService, AdminService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding
                            .UTF8.GetBytes(builder.Configuration.GetRequiredSection("TokenKey").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });



builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("RequiredAdminRole", policy => policy.RequireRole("Admin"));
});

builder.Services.AddIdentityCore<User>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;
})
    .AddRoles<Role>()
    .AddRoleManager<RoleManager<Role>>()
    .AddEntityFrameworkStores<PsinderDb>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<PsinderDb>();
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<Role>>();
    await context.Database.MigrateAsync();
    await Seeder.SeedShelters(context);
    await Seeder.SeedUsers(userManager, roleManager, context);
    
}
catch (Exception ex)
{
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}


app.Run();
