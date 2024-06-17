namespace SampleApp
{
	using System.Reflection;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks;
	using Fluxera.Extensions.Hosting.Modules.OpenTelemetry;
	using Fluxera.Extensions.Hosting.Modules.Serilog;
	using Fluxera.Extensions.Hosting.Plugins;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Hosting;
	using Microsoft.Extensions.Logging;
	using Serilog;
	using Serilog.Extensions.Hosting;
	using Serilog.Extensions.Logging;

	internal sealed class SampleAppServiceHost : WebApplicationHost<SampleAppServiceModule>
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
			// Add user secrets configuration source.
			builder.ConfigureAppConfiguration(configurationBuilder =>
			{
				configurationBuilder.AddUserSecrets(Assembly.GetExecutingAssembly());
			});

			// Add OpenTelemetry logging.
			builder.AddOpenTelemetryLogging( /*options => { }*/);

			// Add Serilog logging.
			builder.AddSerilogLogging((context, options) =>
			{
				options
					.WriteTo.Async(x =>
					{
						if (context.HostingEnvironment.IsDevelopment())
						{
							x.Console();
							x.Debug();
						}

						x.File(context.HostingEnvironment.GetDefaultLogFilePath(), rollingInterval: RollingInterval.Day);
					});
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
