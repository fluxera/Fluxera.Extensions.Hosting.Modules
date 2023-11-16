namespace Fluxera.Extensions.Hosting.Modules.HealthChecks.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.OpenTelemetry;
	using global::HealthChecks.OpenTelemetry.Instrumentation;
	using global::OpenTelemetry.Metrics;

	internal sealed class MeterProviderContributor : IMeterProviderContributor
	{
		/// <inheritdoc />
		public void Configure(MeterProviderBuilder builder, IServiceConfigurationContext context)
		{
			builder.AddHealthChecksInstrumentation();
		}
	}
}
