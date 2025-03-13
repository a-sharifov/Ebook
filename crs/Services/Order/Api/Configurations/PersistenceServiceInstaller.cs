using Api.Core.ServiceInstaller.Interfaces;
using Domain.Core.UnitOfWorks.Interfaces;
using Domain.OrderAggregate.Repositories;
using EventBus.MassTransit.Abstractions;
using EventBus.MassTransit.RabbitMQ.Services;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.DbContexts;
using Persistence.Repositories;

namespace Api.Configurations;

internal sealed class PersistenceServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrderDbContext>(options =>
        {
            options.UseNpgsql(Env.POSTGRE_CONNECTION_STRING);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddTransient<IMessageBus, EventBusRabbitMQ>();
    }
}
