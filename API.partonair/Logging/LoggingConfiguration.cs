using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.Grafana.Loki;

namespace API.partonair.Logging
{
    public static class LoggingConfiguration
    {
        public static IHostBuilder ConfigureLogging
            (
                this IHostBuilder builder,
                IConfiguration configuration,
                string applicationName
            )
        {
            builder.UseSerilog((context, services, loggerConfiguration) =>
            {
                loggerConfiguration
                    .MinimumLevel.Information()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .Enrich.FromLogContext()
                    .Enrich.WithExceptionDetails()
                    .Enrich.WithMachineName()
                    .Enrich.WithThreadId()
                    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
                    .WriteTo.GrafanaLoki(
                        uri: configuration["Logging:Loki:Url"] ?? throw new ArgumentNullException("Key 'Logging:LokiUrl' is empty"),
                        labels: new[] {
                            new LokiLabel { Key = "app", Value = applicationName },
                            new LokiLabel { Key = "env", Value = context.HostingEnvironment.EnvironmentName }
                        });
            });


            return builder;
        }
    }
}

