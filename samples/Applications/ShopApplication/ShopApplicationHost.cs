#define MONOLITH

namespace ShopApplication
{
	using Catalog.Application;
	using Catalog.MessagingApi;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks;
	using Fluxera.Extensions.Hosting.Modules.OpenTelemetry;
	using Fluxera.Extensions.Hosting.Modules.Serilog;
	using Fluxera.Extensions.Hosting.Plugins;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Hosting;
	using Microsoft.Extensions.Logging;
	using OpenTelemetry.Logs;
	using Ordering.Application;
	using Ordering.MessagingApi;
	using Serilog;
	using Serilog.Extensions.Hosting;
	using Serilog.Extensions.Logging;

	public class ShopApplicationHost : WebApplicationHost<ShopApplicationModule>
	{
		/// <inheritdoc />
		protected override void ConfigureApplicationPlugins(IPluginConfigurationContext context)
		{
			context.AddPlugin<SerilogModule>();
			context.AddPlugin<AspNetCoreHealthChecksModule>();

#if MONOLITH
			// Configure the Catalog component.
			context.AddPlugin<CatalogApplicationModule>();
			context.AddPlugin<CatalogMessagingApiModule>();

			// Configure the Ordering component.
			context.AddPlugin<OrderingApplicationModule>();
			context.AddPlugin<OrderingMessagingApiModule>();
#else
			// Configure the Catalog component.
			context.AddPlugin<CatalogHttpApiModule>();

			// Configure the Ordering component.
			context.AddPlugin<OrderingHttpApiModule>();
#endif
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
