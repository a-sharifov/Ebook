using Api;
using Api.Extensions;

CreateHostBuilder(args).Build().Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
       .UseLog()
       .ConfigureWebHostDefaults(webBuilder =>
      {
          webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
          {
              config.AddYamlFile(
                  "appsettings.yml", optional: true, reloadOnChange: true);
          });

          webBuilder.UseStartup<Startup>();
      });
