using Api.Core.ServiceInstaller.Interfaces;
using Api.OptionsSetup;
using Domain.Core.UnitOfWorks.Interfaces;
using Infrastructure.Email;
using Infrastructure.Email.Interfaces;
using Infrastructure.Email.Services;
using Infrastructure.Hashing;
using Infrastructure.Hashing.Interfaces;
using Infrastructure.Jwt;
using Infrastructure.Jwt.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.DbContexts;

namespace Api.Configurations;

internal sealed class InfrastructureServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BookDbContext>(options =>
        options.UseNpgsql(Env.POSTGRE_CONNECTION_STRING));

        services.AddStackExchangeRedisCache(options =>
        options.Configuration = Env.REDIS_CONNECTION_STRING);

        //TODO: fix problem with Scrutor
        //services.Scan(selector =>
        //selector.FromAssemblies(
        //    Infrastructure.AssemblyReference.Assembly,
        //    Persistence.AssemblyReference.Assembly)
        //.AddClasses()
        //.UsingRegistrationStrategy(RegistrationStrategy.Skip)
        //.AsImplementedInterfaces()
        //.WithScopedLifetime());

        services.AddTransient<IIdentityEmailService, IdentityEmailService>();
        services.AddTransient<IHashingService, HashingService>();
        services.AddTransient<IJwtManager, JwtManager>();

        services.AddTransient<IUnitOfWork, UnitOfWork>();

        services.AddOptions<EmailOptions>()
           .Bind(configuration.GetSection(SD.EmailSectionKey))
           .ValidateDataAnnotations();

        services.ConfigureOptions<IdentityEndpointOptionsSetup>();
    }
}
