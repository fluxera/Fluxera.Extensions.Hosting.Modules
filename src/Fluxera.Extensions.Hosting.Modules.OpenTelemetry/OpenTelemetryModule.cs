namespace Fluxera.Extensions.Hosting.Modules.OpenTelemetry
{
	using System;
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.OpenTelemetry.Contributors;
	using global::OpenTelemetry.Metrics;
	using global::OpenTelemetry.Trace;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A module that enables OpenTelemetry monitoring.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(ConfigurationModule))]
	public sealed class OpenTelemetryModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();

			// Add the contributor list.
			context.Log("AddObjectAccessor(MeterProviderContributorList)",
				services => services.AddObjectAccessor(new MeterProviderContributorList(), ObjectAccessorLifetime.ConfigureServices));

			// Add the contributor list.
			context.Log("AddObjectAccessor(ConfigureContributorList)",
				services => services.AddObjectAccessor(new TracerProviderContributorList(), ObjectAccessorLifetime.ConfigureServices));

			// Add the meter provider contributor.
			context.Services.AddMeterProviderContributor<MeterProviderContributor>();
		}

		/// <inheritdoc />
		public override void PostConfigureServices(IServiceConfigurationContext context)
		{
			OpenTelemetryOptions telemetryOptions = context.Services.GetOptions<OpenTelemetryOptions>();

			// Configure the OpenTelemetry metrics.
			MeterProviderContributorList meterProviderContributorList = context.Services.GetObject<MeterProviderContributorList>();
			context.Services.AddOpenTelemetryMetrics(builder =>
			{
				foreach(IMeterProviderContributor contributor in meterProviderContributorList)
				{
					contributor.Configure(builder);
				}

				//builder.AddAspNetCoreInstrumentation();
				builder.AddMeter($"{context.Environment.ApplicationName}");
				builder.AddOtlpExporter(options => options.Endpoint = new Uri(telemetryOptions.OpenTelemetryProtocolEndpoint));
			});

			// Configure the OpenTelemetry tracing.
			TracerProviderContributorList tracerProviderContributorList = context.Services.GetObject<TracerProviderContributorList>();
			context.Services.AddOpenTelemetryTracing(builder =>
			{
				foreach(ITracerProviderContributor contributor in tracerProviderContributorList)
				{
					contributor.Configure(builder);
				}

				//builder.AddAspNetCoreInstrumentation();
				builder.AddSource($"{context.Environment.ApplicationName}");
				builder.AddOtlpExporter(options => options.Endpoint = new Uri(telemetryOptions.OpenTelemetryProtocolEndpoint));
			});
		}
	}
}
