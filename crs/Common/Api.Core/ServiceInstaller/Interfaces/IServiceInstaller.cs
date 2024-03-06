using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Interface for service installers.
/// </summary>
namespace Api.Core.ServiceInstaller.Interfaces;

public interface IServiceInstaller
{
    /// <summary>
    /// Installs the services.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection" />.</param>
    /// <param name="configuration"> The <see cref="IConfiguration" />.</param>
    void Install(IServiceCollection services, IConfiguration configuration);
}
