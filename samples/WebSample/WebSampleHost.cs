namespace WebSample
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules.OpenTelemetry;
	using OpenTelemetry.Logs;

	public class WebSampleHost : WebApplicationHost<WebSampleModule>
	{
		/// <inheritdoc />
		protected override void ConfigureHostBuilder(IHostBuilder builder)
		{
			// Add OpenTelemetry logging.
			builder.AddOpenTelemetryLogging((hostingContext, loggerOptions) =>
			{
				loggerOptions.AddConsoleExporter();
			});
		}
	}
}
