namespace WebSample
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules.OpenTelemetry;
	using Fluxera.Extensions.Hosting.Modules.Serilog;
	using OpenTelemetry.Logs;
	using Serilog;
	using Serilog.Extensions.Hosting;
	using Serilog.Extensions.Logging;

	public class WebSampleHost : WebApplicationHost<WebSampleModule>
	{
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
