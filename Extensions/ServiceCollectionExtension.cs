using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Psinder.Data;
using Psinder.Middleware;
using Psinder.Repositories.Interfaces;
using Psinder.Repositories;
using Psinder.Services.Interfaces;
using Psinder.Services;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Psinder.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<PsinderDb>(
                options => options.UseSqlServer(config.GetConnectionString("Psinder")));
            services.AddCors();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IShelterRepository, ShelterRepository>();
            services.AddScoped<IShelterService, ShelterService>();
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAnimalRepository, AnimalRepository>();
            services.AddScoped<IAnimalService, AnimalService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<ILikeService, LikeService>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuerSigningKey = true,
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding
                                        .UTF8.GetBytes(config.GetRequiredSection("TokenKey").Value)),
                                    ValidateIssuer = false,
                                    ValidateAudience = false
                                };
                            });



            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("RequiredAdminRole", policy => policy.RequireRole("Admin"));
                opt.AddPolicy("RequiredShelterOwnerRole", policy => policy.RequireRole("ShelterOwner"));
                opt.AddPolicy("RequiredShelterWorkerRole", policy => policy.RequireRole("ShelterWorker"));
            });

            services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            })
                .AddRoles<Role>()
                .AddRoleManager<RoleManager<Role>>()
                .AddEntityFrameworkStores<PsinderDb>();

            return services;
        }
    }
}
