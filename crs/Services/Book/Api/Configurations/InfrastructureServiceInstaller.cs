using Api.Core.ServiceInstaller.Interfaces;
using Api.OptionsSetup;
using Domain.Core.UnitOfWorks.Interfaces;
using EventBus.MassTransit.Abstractions;
using EventBus.MassTransit.RabbitMQ.Services;
using Infrastructure.FileStorages.Interfaces;
using Infrastructure.FileStorages.Services;
using Infrastructure.Hashing;
using Infrastructure.Hashing.Interfaces;
using Infrastructure.Jwt.Interfaces;
using Infrastructure.Jwt.Services;
using Infrastructure.Seeds;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.DbContexts;

namespace Api.Configurations;

internal sealed class InfrastructureServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BookDbContext>(options =>
         options.UseNpgsql(Env.POSTGRE_CONNECTION_STRING)
         //.UseTriggers(x => x.AddTrigger<BookItemBeforeTrigger>())
         );

        //TODO: fix problem with Scrutor
        //services.Scan(selector =>
        //selector.FromAssemblies(
        //    Infrastructure.AssemblyReference.Assembly,
        //    Persistence.AssemblyReference.Assembly)
        //.AddClasses()
        //.UsingRegistrationStrategy(RegistrationStrategy.Skip)
        //.AsImplementedInterfaces()
        //.WithScopedLifetime());

        services.AddTransient<SeedDefaultProject>();
        services.AddTransient<IHashingService, HashingService>();
        services.AddTransient<IJwtManager, JwtManager>();
        services.AddTransient<IJwtBlacklistManager, JwtBlacklistManager>();
        services.AddTransient<IFileService, MinioService>();

        services.AddTransient<IMessageBus, EventBusRabbitMQ>();

        services.AddTransient<IUnitOfWork, UnitOfWork>();

        services.ConfigureOptions<BaseUrlOptionsSetup>();
        services.ConfigureOptions<IdentityEndpointOptionsSetup>();
    }
}
