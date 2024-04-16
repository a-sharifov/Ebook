using Api.Core.ServiceInstaller.Interfaces;
using Api.OptionsSetup;
using Contracts.Enum;
using Contracts.Enumerations;
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
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, configureOptions =>
            {
                configureOptions.RequireHttpsMetadata = true;
                configureOptions.SaveToken = true;
                configureOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = Env.AUTH_ISSUER,
                    ValidateAudience = true,
                    ValidAudiences = [Env.WEB_AUDIENCE],
                    RoleClaimType = ClaimTypes.Role,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Env.JWT_SECURITY_KEY)),
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policy.Admin, policy =>
           policy.RequireRole(Role.Admin));

            options.AddPolicy(Policy.User, policy =>
           policy.RequireRole(Role.User));

            options.AddPolicy(Policy.UserAndAdmin, policy =>
            policy.RequireRole(Role.User, Role.Admin));
        });

        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();
    }
}
