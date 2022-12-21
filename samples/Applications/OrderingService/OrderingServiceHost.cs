namespace OrderingService
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks;
	using Fluxera.Extensions.Hosting.Modules.OpenTelemetry;
	using Fluxera.Extensions.Hosting.Modules.Serilog;
	using Fluxera.Extensions.Hosting.Plugins;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Hosting;
	using Microsoft.Extensions.Logging;
	using OpenTelemetry.Logs;
	using Serilog;
	using Serilog.Extensions.Hosting;
	using Serilog.Extensions.Logging;

	public class OrderingServiceHost : WebApplicationHost<OrderingServiceModule>
	{
		/// <inheritdoc />
		protected override void ConfigureApplicationPlugins(IPluginConfigurationContext context)
		{
			context.AddPlugin<SerilogModule>();
			context.AddPlugin<HealthChecksEndpointsModule>();
		}

		/// <inheritdoc />
		protected override void ConfigureHostBuilder(IHostBuilder builder)
		{
			// Add OpenTelemetry logging.
			builder.AddOpenTelemetryLogging(options =>
			{
				options.AddConsoleExporter();
			});

			// Add Serilog logging
			builder.AddSerilogLogging((context, options) =>
			{
				options
					.ReadFrom.Configuration(context.Configuration)
					.Enrich.FromLogContext()
					.WriteTo.Console();
			});
		}

		/// <inheritdoc />
		protected override ILoggerFactory CreateBootstrapperLoggerFactory(IConfiguration configuration)
		{
			ReloadableLogger bootstrapLogger = new LoggerConfiguration()
				.ReadFrom.Configuration(configuration)
				.Enrich.FromLogContext()
				.WriteTo.Console()
				.CreateBootstrapLogger();

			ILoggerFactory loggerFactory = new SerilogLoggerFactory(bootstrapLogger);
			return loggerFactory;
		}
	}
}
