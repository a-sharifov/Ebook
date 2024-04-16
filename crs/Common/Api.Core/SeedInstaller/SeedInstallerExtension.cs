using Contracts.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Infrastructure.Core.Seeds;

namespace Api.Core.SeedInstaller;

public static class SeedInstallerExtension
{
    public static void InstallSeed<TSeed>(
        this IApplicationBuilder services) 
       where  TSeed : ISeeder  
    {
        using var scope = services.ApplicationServices.CreateScope();
        var seed = scope.ServiceProvider.GetRequiredService<TSeed>();
        seed.Seed();
    }
}
