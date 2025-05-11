using Application.Abstractions;
using Domain.Repositories;
using Domain.ValueObjects;
using Infrastructure.Authentication;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => {
            options.UseNpgsql(configuration.GetConnectionString("DatabaseConnection"));
        });


        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtSection = configuration.GetSection("Jwt");
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtSection.GetValue<string>("Issuer"),
                    ValidAudience = jwtSection.GetValue<string>("Audience"),
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection.GetValue<string>("SecretKey")))
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(PolicyList.AdminPolicy, policy =>
                policy.RequireClaim(ClaimTypes.Role, Role.Admin));
        });

        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IDriverRepository, DriverRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IHashingProvider, HashingProvider>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IRefreshTokenProvider, RefreshTokenProvider>();
        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));



        return services;
    }
}
