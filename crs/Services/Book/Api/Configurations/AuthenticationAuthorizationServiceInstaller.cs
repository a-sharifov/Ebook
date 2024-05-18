﻿using Api.Core.ServiceInstaller.Interfaces;
using Api.OptionsSetup;
using Contracts.Enum;
using Contracts.Enumerations;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Api.Configurations;

internal sealed class AuthenticationAuthorizationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = Env.AUTH_ISSUER,
                    ValidateAudience = false,
                    //ValidAudiences = [Env.WEB_AUDIENCE],
                    RoleClaimType = ClaimTypes.Role,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Env.JWT_SECURITY_KEY)),
                };
            });

        services.AddAuthorizationBuilder()
                     .AddPolicy(Policy.Admin, policy =>
           policy.RequireRole(Role.Admin.Name))
                     .AddPolicy(Policy.User, policy =>
           policy.RequireRole(Role.User.Name))
                     .AddPolicy(Policy.UserAndAdmin, policy =>
            policy.RequireRole(Role.User.Name, Role.Admin.Name));

        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();
    }
}
