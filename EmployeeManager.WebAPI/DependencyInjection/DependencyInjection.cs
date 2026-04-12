using EmployeeManager.Domain.Repositories.Implements;
using EmployeeManager.Domain.Repositories.Interface;
using EmployeeManager.Domain.Services.Implements;
using EmployeeManager.Domain.Services.Interface;
using EmployeeManager.Entity.Entities;
using EmployeeManager.WebAPI.AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NReco.Logging.File;
using System.Text;


namespace EmployeeManager.WebAPI.DependencyInjection
{
    public class DependencyInjection
    {
        public WebApplicationBuilder InitBuilder(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

            var JWTKey = builder.Configuration.GetSection("JsonWebToken:SecretKey").Value;

            if(JWTKey == null) { 
                throw new NullReferenceException("JWT Secret Key is not configured.");
            }

            var JWTByteKey = Encoding.ASCII.GetBytes(JWTKey);

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Configure Entity Framework Core with SQL Server
            builder.Services.AddDbContext<EmployeeManagerContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register the Generic Base Repository
            builder.Services.AddScoped(typeof(IEmployeeRepository), typeof(EmployeeRepository));
            builder.Services.AddScoped(typeof(IVLoginRepository), typeof(VLoginRepository));

            // Register Specific Domain Services
            builder.Services.AddScoped(typeof(IEmployeeService), typeof(EmployeeService));
            builder.Services.AddScoped(typeof(IVLoginService), typeof(VLoginService));

            // Register Mapper
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            // Authentication and Authorization
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    RoleClaimType = "userRole",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(JWTByteKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // Register Logging (will implement later)
            

            return builder;
        }
    }
}
