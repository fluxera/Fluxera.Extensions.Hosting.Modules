namespace ExampleService
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules.Messaging.InMemory;
	using Fluxera.Extensions.Hosting.Modules.OpenTelemetry;
	using Fluxera.Extensions.Hosting.Modules.Persistence.InMemory;
	using Fluxera.Extensions.Hosting.Modules.Serilog;
	using Fluxera.Extensions.Hosting.Plugins;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Hosting;
	using Microsoft.Extensions.Logging;
	using OpenTelemetry.Logs;
	using Serilog;
	using Serilog.Extensions.Hosting;
	using Serilog.Extensions.Logging;

	public class ExampleServiceHost : WebApplicationHost<ExampleServiceModule>
	{
		/// <inheritdoc />
		protected override void ConfigureApplicationPlugins(IPluginConfigurationContext context)
		{
			context
				.AddPlugin<SerilogModule>()
				.AddPlugin<InMemoryPersistenceModule>()
				.AddPlugin<InMemoryMessagingModule>();
		}

		/// <inheritdoc />
		protected override void ConfigureHostBuilder(IHostBuilder builder)
		{
			// Add OpenTelemetry logging.
			builder.AddOpenTelemetryLogging(loggerOptions =>
			{
				loggerOptions.AddConsoleExporter();
			});

			// Add Serilog logging
			builder.AddSerilogLogging(loggerOptions =>
			{
				loggerOptions
					.MinimumLevel.Information()
					.WriteTo.Console();
			});
		}

		/// <inheritdoc />
		protected override ILoggerFactory CreateBootstrapperLoggerFactory(IConfiguration configuration)
		{
			ReloadableLogger bootstrapLogger = new LoggerConfiguration()
				.Enrich.FromLogContext()
				.ReadFrom.Configuration(configuration)
				.WriteTo.Console()
				.CreateBootstrapLogger();

			ILoggerFactory loggerFactory = new SerilogLoggerFactory(bootstrapLogger);
			return loggerFactory;
		}
	}
}
