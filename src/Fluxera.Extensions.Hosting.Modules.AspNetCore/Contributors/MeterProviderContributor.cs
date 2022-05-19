namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.OpenTelemetry;
	using global::OpenTelemetry.Metrics;

	internal sealed class MeterProviderContributor : IMeterProviderContributor
	{
		/// <inheritdoc />
		public void Configure(MeterProviderBuilder builder, IServiceConfigurationContext context)
		{
			builder.AddAspNetCoreInstrumentation();
		}
	}
}
