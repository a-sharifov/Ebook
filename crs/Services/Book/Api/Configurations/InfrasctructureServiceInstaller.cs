using Api.Core.ServiceInstaller.Interfaces;
using Infrastructure.Email;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContexts;
using Scrutor;

namespace Api.Configurations;

internal sealed class InfrasctructureServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BookDbContext>(options =>
        options.UseNpgsql(Env.POSTGRE_CONNECTION_STRING));

        services.AddStackExchangeRedisCache(options =>
        options.Configuration = Env.REDIS_CONNECTION_STRING);

        services.Scan(selector =>
        selector.FromAssemblies(
            Infrastructure.AssemblyReference.Assembly,
            Persistence.AssemblyReference.Assembly)
        .AddClasses()
        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
        .AsImplementedInterfaces()
        .WithScopedLifetime());

        services.AddOptions<EmailOptions>()
           .Bind(configuration.GetSection(SD.EmailSectionKey))
           .ValidateDataAnnotations();
    }
}
