using Api.Core.ServiceInstaller;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseLog();

builder.Configuration.AddYamlFile("appsettings.yml", optional: true, reloadOnChange: true);
builder.Services.InstallServicesFromAssembly(builder.Configuration, AssemblyReference.Assembly);

var app = builder.Build();

app.Run();

