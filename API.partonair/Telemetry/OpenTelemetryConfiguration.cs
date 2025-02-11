using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;


namespace API.partonair.Telemetry
{
    public static class OpenTelemetryConfiguration
    {
        public static IServiceCollection AddOpenTelemetryTracingAndMetrics(
      this IServiceCollection services,
      IConfiguration configuration,
      string applicationName)
        {
            var resourceBuilder = ResourceBuilder.CreateDefault().AddService(applicationName);

            services.AddOpenTelemetry()
                .WithTracing(builder =>
                {
                    builder
                        .SetResourceBuilder(resourceBuilder)
                        .AddAspNetCoreInstrumentation(options => options.RecordException = true)
                        .AddHttpClientInstrumentation()
                        .AddSqlClientInstrumentation(options =>
                        {
                            options.SetDbStatementForText = true;
                            options.RecordException = true;
                        })
                        .AddOtlpExporter(opts =>
                        {
                            opts.Endpoint = new Uri(configuration["OpenTelemetry:OtlpEndpoint"]);
                        });
                })
                .WithMetrics(builder =>
                {
                    builder
                        .SetResourceBuilder(resourceBuilder)
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddOtlpExporter(opts =>
                        {
                            opts.Endpoint = new Uri(configuration["OpenTelemetry:OtlpEndpoint"]);
                        });
                });

            return services;
        }

    }
}
