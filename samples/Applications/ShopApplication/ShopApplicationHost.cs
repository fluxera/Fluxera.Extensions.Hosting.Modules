#define MONOLITH

namespace ShopApplication
{
	using System.Reflection;
	using Catalog.Application;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks;
	using Fluxera.Extensions.Hosting.Modules.Messaging.RabbitMQ;
	using Fluxera.Extensions.Hosting.Modules.OpenTelemetry;
	using Fluxera.Extensions.Hosting.Modules.Serilog;
	using Fluxera.Extensions.Hosting.Plugins;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Hosting;
	using Microsoft.Extensions.Logging;
	using Ordering.Application;
	using Serilog;
	using Serilog.Extensions.Hosting;
	using Serilog.Extensions.Logging;

	public class ShopApplicationHost : WebApplicationHost<ShopApplicationModule>
	{
		/// <inheritdoc />
		protected override void ConfigureApplicationPlugins(IPluginConfigurationContext context)
		{
			context.AddPlugin<SerilogModule>();
			context.AddPlugin<HealthChecksEndpointsModule>();
			context.AddPlugin<RabbitMqMessagingModule>();

#if MONOLITH
			// Configure the Catalog component.
			context.AddPlugin<CatalogApplicationModule>();

			// Configure the Ordering component.
			context.AddPlugin<OrderingApplicationModule>();
#else
			// Configure the Catalog component.
			context.AddPlugin<CatalogHttpClientModule>();

			// Configure the Ordering component.
			context.AddPlugin<OrderingHttpClientModule>();
#endif
			//context.AddPlugin<MultiTenancyModule>();
		}

		/// <inheritdoc />
		protected override void ConfigureHostBuilder(IHostBuilder builder)
		{
			// Add user secrets configuration source.
			builder.ConfigureAppConfiguration(configurationBuilder =>
			{
				configurationBuilder.AddUserSecrets(Assembly.GetExecutingAssembly());
			});

			// Add Serilog logging.
			builder.AddSerilogLogging((_, options) =>
			{
				options
					.WriteTo.Console();
			});
		}

		/// <inheritdoc />
		protected override ILoggerFactory CreateBootstrapperLoggerFactory(IConfiguration configuration)
		{
			OpenTelemetryOptions telemetryOptions = configuration.Get<OpenTelemetryOptions>();

			ReloadableLogger bootstrapLogger = new LoggerConfiguration()
				.ReadFrom.Configuration(configuration)
				.Enrich.FromLogContext()
				.WriteTo.OpenTelemetry(telemetryOptions.OpenTelemetryProtocolEndpoint)
				.WriteTo.Console()
				.CreateBootstrapLogger();

			ILoggerFactory loggerFactory = new SerilogLoggerFactory(bootstrapLogger);
			return loggerFactory;
		}
	}
}
