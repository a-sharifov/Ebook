using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Api.Core.Middlewares;
using Api.Core.Extensions;
using Api.Core.ServiceInstaller;
using Persistence.DbContexts;
using Serilog;
using Asp.Versioning.ApiExplorer;
using Api.Core.SeedInstaller;
using Infrastructure.Seeds;

namespace Api;

public sealed class Startup(IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;

    public void ConfigureServices(IServiceCollection services) => 
        services.InstallServicesFromAssembly(_configuration, AssemblyReference.Assembly);

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            using var scope = app.ApplicationServices.CreateScope();
            var apiVersionDescriptionProvider = 
                scope.ServiceProvider.GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    x.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });
        }

        app.UseCors(SD.DefaultCorsPolicyName);
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseSerilogRequestLogging();
        app.UseHttpsRedirection();

        app.MigrateDbContext<BookDbContext>();
        app.InstallSeed<SeedDefaultProject>();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        });
    }

}
