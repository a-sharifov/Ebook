using Application.Core.MappingConfig.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Contracts.Extensions;

namespace Application.Core.MappingConfig;

/// <summary>
/// Extension methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class MappingConfigExtensions
{
    /// <summary>
    /// Config mapper from the given assembly.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection" />.</param>
    /// <param name="configuration"> The <see cref="IConfiguration"/>.</param>
    /// <param name="assembly"> The <see cref="Assembly"/>.</param>
    public static void MappingConfigsFromAssembly(
        this IServiceCollection services,
        Assembly assembly) =>
        assembly
        .DefinedTypes
        .Where(IsServiceInstaller)
        .Select(x => (IMappingConfig)Activator.CreateInstance(x)!)
        .Foreach(x => x.Configure());

    /// <summary>
    /// Check if the given type is a service installer.
    /// </summary>
    /// <param name="type"> The <see cref="Type"/>.</param>
    /// <returns></returns>
    private static bool IsServiceInstaller(Type type) =>
        !type.IsInterface &&
        !type.IsAbstract &&
        typeof(IMappingConfig).IsAssignableFrom(type);
}
